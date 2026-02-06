using Personal_Buget_Tracker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp.Gui
{
    public partial class MainMenu : UserControl
    {
        private readonly Form1 _mainForm;
        private readonly BudgetManager _budgetManager;
        private readonly ReportService _reportService;
        public MainMenu(Form1 mainForm, BudgetManager budgetManager, ReportService reportService)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _budgetManager = budgetManager;
            _reportService = reportService;
        }
        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            _mainForm.LoadControl(new AddIncomeControl(_budgetManager));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _mainForm.LoadControl(new AddExpenseControl(_budgetManager));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _mainForm.LoadControl(new ViewIncomesControl(_budgetManager));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var viewExpensesControl = new ViewExpensesControl(_budgetManager);
            _mainForm.LoadControl(viewExpensesControl);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _mainForm.LoadControl(new EditTransactionControl(_budgetManager));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _mainForm.LoadControl(new ReportsAndSummaries(_reportService));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
