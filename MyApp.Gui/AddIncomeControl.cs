using Personal_Buget_Tracker;
using static Personal_Buget_Tracker.BudgetManager;

namespace MyApp.Gui
{
    public partial class AddIncomeControl : UserControl
    {
        private BudgetManager _budgetManager;
        private ReportService _reportService;
        public AddIncomeControl(BudgetManager manager)
        {
            InitializeComponent();
            _budgetManager = manager;
            ViewIncomesCategories();

        }

        private void ViewIncomesCategories()
        {
            comboBox1.Items.Clear();
            var categories = CategoryRepository.IncomeCategories;
            comboBox1.Items.AddRange(categories.ToArray());
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
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

            var category = comboBox1.SelectedItem.ToString();

            Income income = new Income
            {
                Amount = decimal.Parse(textBox1.Text),
                Date = Convert.ToDateTime(dateTimePicker1.Value),
                Description = Convert.ToString(textBox2.Text),
                Category = category
            };

            _budgetManager.IncomeManager.AddIncome(income);
            MessageBox.Show("Income added successfully!");
        }
        private void btnBack_Click(object sender, EventArgs e)
        {

            if (this.Parent is Form1 mainForm)
            {
                mainForm.LoadControl(new MainMenu(mainForm, _budgetManager, _reportService));
            }
        }

        private void AddIncomeControl_Load(object sender, EventArgs e)
        {

        }
    }
}
