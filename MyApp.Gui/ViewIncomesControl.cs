using Personal_Buget_Tracker;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MyApp.Gui
{
    public partial class ViewIncomesControl : UserControl
    {
        private readonly BudgetManager? _budgetManager;
        private readonly ReportService _reportService;

        public ViewIncomesControl()
        {
            InitializeComponent();
            ConfigureListView();
        }

        public ViewIncomesControl(BudgetManager budgetManager) : this()
        {
            _budgetManager = budgetManager;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_budgetManager != null && !DesignMode)
            {
                LoadIncomes();
            }
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

        private void LoadIncomes()
        {
            if (_budgetManager == null)
                return;

            var incomes = _budgetManager.IncomeManager?.GetAllIncomes();

            if (incomes == null || !incomes.Any())
            {
                MessageBox.Show("No incomes recorded yet.", "Information");
                return;
            }

            listView1.Items.Clear();

            foreach (var income in incomes)
            {
                listView1.Items.Add(new ListViewItem(new[]
                {
                    income.Id.ToString(),
                    income.Date.ToString("yyyy-MM-dd"),
                    income.Amount.ToString("C"),
                    income.Category?.ToString() ?? "",
                    income.Description ?? ""
                }));
            }
            label2.Text = $"Total Amount: {incomes.Sum(i => i.Amount):C}";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ViewIncomesControl_Load(object sender, EventArgs e)
        {

        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Parent is Form1 mainForm)
            {
                mainForm.LoadControl(new MainMenu(mainForm, _budgetManager, _reportService));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}