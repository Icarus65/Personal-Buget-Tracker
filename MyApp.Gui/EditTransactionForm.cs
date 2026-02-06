using Personal_Buget_Tracker;
using System;
using System.Linq;
using System.Windows.Forms;
using static Personal_Buget_Tracker.BudgetManager;

namespace MyApp.Gui
{
    public partial class EditTransactionForm : Form
    {
        private readonly Income _income;
        private readonly IncomeManager _incomeManager;
        private readonly Expense _expense;
        private readonly ExpenseManager _expenseManager;

        public EditTransactionForm(Income income, IncomeManager incomeManager)
        {
            InitializeComponent();
            _income = income ?? throw new ArgumentNullException(nameof(income));
            _incomeManager = incomeManager ?? throw new ArgumentNullException(nameof(incomeManager));

            txtDescription.Text = _income.Description;
            txtAmount.Text = _income.Amount.ToString("F2");
            dtpDate.Value = _income.Date;

            PopulateCategoryComboBox();
        }

        public EditTransactionForm(Expense expense, ExpenseManager expenseManager)
        {
            InitializeComponent();
            _expense = expense ?? throw new ArgumentNullException(nameof(expense));
            _expenseManager = expenseManager ?? throw new ArgumentNullException(nameof(expenseManager));

            txtDescription.Text = _expense.Description;
            txtAmount.Text = _expense.Amount.ToString("F2");
            dtpDate.Value = _expense.Date;

            PopulateCategoryComboBox();
        }

        private void PopulateCategoryComboBox()
        {
            cmbCategory.Items.Clear();

            if (_income != null)
            {
                cmbCategory.Items.AddRange(CategoryRepository.IncomeCategories);
                cmbCategory.Text = _income.Category ?? CategoryRepository.IncomeCategories[0];

            }
            else if (_expense != null)
            {
                cmbCategory.Items.AddRange(CategoryRepository.ExpenseCategories);
                cmbCategory.Text = _expense.Category ?? CategoryRepository.ExpenseCategories[0];
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate description
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a description.");
                return;
            }

            // Validate amount
            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            string category = cmbCategory.Text.Trim();

            if (_income != null)
            {
                _income.Description = txtDescription.Text.Trim();
                _income.Amount = amount;
                _income.Date = dtpDate.Value;
                _income.Category = category;

                _incomeManager.UpdateIncome(_income);
                MessageBox.Show("Income updated successfully!");
            }
            else if (_expense != null)
            {
                _expense.Description = txtDescription.Text.Trim();
                _expense.Amount = amount;
                _expense.Date = dtpDate.Value;
                _expense.Category = category;

                _expenseManager.UpdateExpense(_expense);
                MessageBox.Show("Expense updated successfully!");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void EditTransactionForm_Load(object sender, EventArgs e) { }
    }
}
