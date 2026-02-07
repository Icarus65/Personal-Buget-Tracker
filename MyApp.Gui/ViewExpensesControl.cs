using Personal_Buget_Tracker;

namespace MyApp.Gui
{
    public partial class ViewExpensesControl : UserControl
    {
        private readonly BudgetManager? _budgetManager;
        private ReportService _reportService;

        public ViewExpensesControl()
        {
            InitializeComponent();
            ConfigureListView();
        }

        public ViewExpensesControl(BudgetManager budgetManager) : this()
        {
            _budgetManager = budgetManager;
        }

        private void ConfigureListView()
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            listView1.Columns.Clear();
            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("Date", 100);
            listView1.Columns.Add("Amount", 100);
            listView1.Columns.Add("Category", 100);
            listView1.Columns.Add("Description", 200);
        }

        private void LoadExpenses()
        {
            if (_budgetManager == null)
                return;

            var expenses = _budgetManager.ExpenseManager.GetAllExpenses();

            if (!expenses.Any())
            {
                MessageBox.Show("No expenses recorded yet.");
                return;
            }

            listView1.Items.Clear();

            foreach (var expense in expenses)
            {
                listView1.Items.Add(new ListViewItem(new[]
                {
                    expense.Id.ToString(),
                    expense.Date.ToString("yyyy-MM-dd"),
                    expense.Amount.ToString("C"),
                    expense.Category.ToString(),
                    expense.Description.ToString()
                }));
            }
            label2.Text = $"Total Amount: {expenses.Sum(e => e.Amount):C}";
        }

        private void ViewExpensesControl_Load(object sender, EventArgs e)
        {
            LoadExpenses();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Parent is Form1 mainForm)
            {
                // Call a method in the main form to show the previous control
                mainForm.LoadControl(new MainMenu(mainForm, _budgetManager, _reportService));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
