using Personal_Buget_Tracker;
using static Personal_Buget_Tracker.BudgetManager;

namespace MyApp.Gui
{
    public partial class AddExpenseControl : UserControl
    {
        private BudgetManager _budgetManager;
        //private MenuManager _menuManager;
        private ReportService _reportService;

        public AddExpenseControl(BudgetManager manager)
        {
            InitializeComponent();
            _budgetManager = manager;
            ViewExpensesCategories();
        }

        public void ViewExpensesCategories()
        {
            comboBox1.Items.Clear();

            var categories = CategoryRepository.ExpenseCategories;
            comboBox1.Items.AddRange(categories.ToArray());

            if (comboBox1.Items.Count > 0)  comboBox1.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (!decimal.TryParse(textBox1.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            var catergory = comboBox1.SelectedItem.ToString();
            Expense expense = new Expense
            {
                Amount = amount,
                Date = dateTimePicker1.Value,
                Category = catergory,
                Description = textBox2.Text.ToString()
            };

            _budgetManager.ExpenseManager.AddExpense(expense);

            MessageBox.Show("Expense added successfully!");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void AddExpenseControl_Load(object sender, EventArgs e)
        {
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Parent is Form1 mainForm) 
            {
                mainForm.LoadControl(new MainMenu(mainForm, _budgetManager, _reportService));
            }
        }

    }
}
