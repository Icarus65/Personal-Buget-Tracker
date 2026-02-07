using Personal_Buget_Tracker;

namespace MyApp.Gui
{
    public partial class EditTransactionControl : UserControl
    {
        private readonly BudgetManager _budgetManager;
        private readonly ReportService _reportService;

        public EditTransactionControl()
        {

        }

        public EditTransactionControl(BudgetManager budgetManager) : this()
        {
            if(budgetManager == null) throw new ArgumentNullException(nameof(budgetManager));
            _budgetManager = budgetManager;
            InitializeComponent();
            ConfigureListView();
        }

        private void EditTransactionControl_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Income");
            comboBox1.Items.Add("Expense");
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateListView();
        }

        private void PopulateListView()
        {
            if (_budgetManager == null || listViewTransactions == null) return;

            listViewTransactions.Items.Clear();
            string selectedType = comboBox1.SelectedItem?.ToString() ?? "";

            if (selectedType == "Income")
            {
                var incomes = _budgetManager.IncomeManager?.GetAllIncomes() ?? new List<Income>();
                foreach (var income in incomes)
                {
                    if (income == null) continue;

                    var row = new ListViewItem(income.Id.ToString());
                    row.SubItems.Add(income.Date.ToShortDateString());
                    row.SubItems.Add(income.Amount.ToString("C"));
                    row.SubItems.Add(income.Category?.ToString() ?? "");
                    row.SubItems.Add(income.Description ?? "");
                    row.Tag = income;
                    listViewTransactions.Items.Add(row);
                }
            }
            else if (selectedType == "Expense")
            {
                var expenses = _budgetManager.ExpenseManager?.GetAllExpenses() ?? new List<Expense>();
                foreach (var expense in expenses)
                {
                    if (expense == null) continue;

                    var row = new ListViewItem(expense.Id.ToString());
                    row.SubItems.Add(expense.Date.ToShortDateString());
                    row.SubItems.Add(expense.Amount.ToString("C"));
                    row.SubItems.Add(expense.Category?.ToString() ?? "");
                    row.SubItems.Add(expense.Description ?? "");
                    row.Tag = expense;
                    listViewTransactions.Items.Add(row);
                }
            }
        }
        private void ConfigureListView()
        {
            listViewTransactions.View = View.Details;
            listViewTransactions.FullRowSelect = true;
            listViewTransactions.GridLines = true;

            listViewTransactions.Columns.Clear();
            listViewTransactions.Columns.Add("ID", 50);
            listViewTransactions.Columns.Add("Date", 100);
            listViewTransactions.Columns.Add("Amount", 100);
            listViewTransactions.Columns.Add("Category", 100);
            listViewTransactions.Columns.Add("Description", 200);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listViewTransactions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a transaction to edit.");
                return;
            }

            var selectedItem = listViewTransactions.SelectedItems[0];
            string selectedType = comboBox1.SelectedItem?.ToString() ?? "";

            if (selectedType == "Income" && selectedItem.Tag is Income income)
            {
                    OpenEditForm(income);
            }
            else if (selectedType == "Expense" && selectedItem.Tag is Expense expense)
            {
                    OpenEditForm(expense);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewTransactions.SelectedItems.Count == 0) return;

            var selectedItem = listViewTransactions.SelectedItems[0];
            int id = int.Parse(selectedItem.Text);
            string selectedType = comboBox1.SelectedItem?.ToString() ?? "";

            if (selectedType == "Income")
            {
                _budgetManager.IncomeManager.DeleteIncome(id);
            }
            else if (selectedType == "Expense")
            {
                _budgetManager.ExpenseManager.DeleteExpense(id);
            }

            PopulateListView();
        }

        private void OpenEditForm(Income income)
        {
            using var form = new EditTransactionForm(income, _budgetManager.IncomeManager);
            if (form.ShowDialog() == DialogResult.OK)
            {
                PopulateListView();
            }
        }

        private void OpenEditForm(Expense expense)
        {
            using var form = new EditTransactionForm(expense, _budgetManager.ExpenseManager);
            if (form.ShowDialog() == DialogResult.OK)
            {
                PopulateListView();
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Parent is Form1 mainForm) 
            {
                mainForm.LoadControl(new MainMenu(mainForm, _budgetManager, _reportService));
            }
        }
        private void EditTransactionControl_Load_1(object sender, EventArgs e)
        {

        }
    }
}
