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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyApp.Gui
{
    public partial class ReportsAndSummaries : UserControl
    {
        private ReportService _reportService;
        private BudgetManager _budgetManager;
        public ReportsAndSummaries()
        {
            InitializeComponent();
            ComboboxItems();
        }
        public ReportsAndSummaries(ReportService reportService) : this()
        {
            _reportService = reportService;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        public void ComboboxItems()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Monthly Summary");
            comboBox1.Items.Add("Expenses by category");
            comboBox1.Items.Add("Report by date range");
            comboBox1.Items.Add("Top spending categories");
            comboBox1.Items.Add("Savings rate");
            comboBox1.Items.Add("Income by category");
            comboBox1.Items.Add("Year to date summary");
            dateTimePicker1.Value = DateTime.Today.AddDays(-1);
            dateTimePicker2.Value = DateTime.Today;
        }

        private void DisplayMonthlySummaryGUI()
        {
            listView1.Clear();
            dateTimePicker2.Hide();
            int month = dateTimePicker1.Value.Month;
            int year = dateTimePicker1.Value.Year;
            var monthlySummary = _reportService.GetMonthlySummary(month, year);
            label5.Text = $"Monthly Summary for {month}/{year}:\nTotal Income: {monthlySummary.TotalIncome:C}\nTotal Expenses: {monthlySummary.TotalExpenses:C}\nNet Savings: {monthlySummary.NetSavings:C}";
        }

        private void ExpensesByCategoryReportGUI()
        {
            var data = _reportService.GetExpenensesByCategory();

            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            label5.Text = "";
            listView1.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("Category", 150);
            listView1.Columns.Add("Total", 100);

            if (!data.Any())
            {
                MessageBox.Show("No expenses recorded.");
                return;
            }

            foreach (var item in data)
            {
                var row = new ListViewItem(item.Category);
                row.SubItems.Add(item.Total.ToString("C"));
                listView1.Items.Add(row);
            }
        }

        private void ReportByDateRangeGUI()
        {
            listView1.Clear();
            dateTimePicker1.Show();
            dateTimePicker2.Show();
            var report = _reportService.GetReportByDateRange(dateTimePicker1.Value, dateTimePicker2.Value);
            label5.Text = $"Report from {dateTimePicker1.Value.ToShortDateString()} to {dateTimePicker2.Value.ToShortDateString()}:\nTotal Income: {report.TotalIncome:C}\nTotal Expenses: {report.TotalExpenses:C}\nNet Balance: {report.NetBalance:C}";
        }

        private void TopSpendingCategoriesReportGUI()
        {
            var data = _reportService.GetTopSpendingsCategories();
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            label5.Text = "";
            listView1.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("Category", 150);
            listView1.Columns.Add("Total", 100);

            if (!data.Any())
            {
                MessageBox.Show("No expenses recorded.");
                return;
            }

            foreach (var item in data)
            {
                var row = new ListViewItem(item.Category);
                row.SubItems.Add(item.Total.ToString("C"));
                listView1.Items.Add(row);
            }
        }

        private void SavingsRateReportGUI()
        {
            listView1.Clear();

            var savingsRate = _reportService.GetSavingsRate();
            label5.Text = $"Savings Rate: {savingsRate:F2}%";
        }

        private void IncomeByCategoryReportGUI()
        {
            var data = _reportService.GetIncomesByCategory();
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            label5.Text = "";
            listView1.Clear();
            listView1.View = View.Details;
            listView1.Columns.Add("Category", 150);
            listView1.Columns.Add("Total", 100);

            if(!data.Any())
            {
                MessageBox.Show("No incomes recorded.");
                return;
            }

            foreach (var item in data)
            {
                var row = new ListViewItem(item.Category);
                row.SubItems.Add(item.Total.ToString("C"));
                listView1.Items.Add(row);
            }
        }

        private void YearToDateSummaryReport()
        {
            var time = dateTimePicker1.Value;
            var data = _reportService.GetYearToDateSummary();
            listView1.Clear();

            if (DateTime.Now < time)
                MessageBox.Show("Date cannot be in the future.");
            if (time.Year != DateTime.Now.Year)
                MessageBox.Show("Date must be within the current year.");
            label5.Text = $"Year to Date Summary:\nTotal Income: {data.TotalIncome:C}\nTotal Expenses: {data.TotalExpenses:C}\nNet Savings: {data.NetBalance:C}";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem?.ToString())
            {
                case "Monthly Summary":
                    DisplayMonthlySummaryGUI();
                    break;
                case "Expenses by category":
                    ExpensesByCategoryReportGUI();
                    break;
                case "Report by date range":
                    ReportByDateRangeGUI();
                    break;
                case "Top spending categories":
                    TopSpendingCategoriesReportGUI();
                    break;
                case "Savings rate":
                    SavingsRateReportGUI();
                    break;
                case "Income by category":
                    IncomeByCategoryReportGUI();
                    break;
                case "Year to date summary":
                    YearToDateSummaryReport();
                    break;
                default:
                    MessageBox.Show("Please select a valid report type.");
                    break;
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem == null)
                return;

            switch (comboBox1.SelectedItem?.ToString())
            {
                case "Monthly Summary":
                    DisplayMonthlySummaryGUI();
                    break;
                case "Report by date range":
                    ReportByDateRangeGUI();
                    break;
                case "Expenses by category":
                    ExpensesByCategoryReportGUI();
                    break;
                case "Top spending categories":
                    TopSpendingCategoriesReportGUI();
                    break;
                case "Savings rate":
                    SavingsRateReportGUI();
                    break;
                case "Income by category":
                    IncomeByCategoryReportGUI();
                    break;
                case "Year to date summary":
                    YearToDateSummaryReport();
                    break;
                default:
                    MessageBox.Show("Please select a valid report type.");
                    break;
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Parent is Form1 mainForm)
            {
                mainForm.LoadControl(new MainMenu(mainForm, _budgetManager, _reportService));
            }
        }
        private void ReportsAndSummaries_Load(object sender, EventArgs e)
        {

        }
    }
}
