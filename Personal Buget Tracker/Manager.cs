using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Personal_Budget_Tracker
{
    public class IncomeManager
    {
        private List<Income> incomes = new List<Income>();
        private int nextId;
        private DataService dataService;
        public IncomeManager(DataService dataService)
        {
            this.dataService = dataService;
            LoadData();
        }

        private void SaveData()
        {
            dataService.SaveIncomes(incomes);
            var settings = dataService.LoadSettings();
            settings.NextIncomeId = nextId;
            dataService.SaveSettings(settings);
        }

        private void LoadData()
        {
            incomes = dataService.LoadIncomes();
            var settings = dataService.LoadSettings();
            nextId = settings.NextIncomeId;

            if (incomes.Any())
            {
                int maxId = incomes.Max(i => i.Id);
                if (nextId <= maxId)
                    nextId = maxId + 1;
            }
        }

        public void AddIncome(Income income)
        {
            ValidateIncome(income);
            ValidateDate(income.Date);

            income.Id = nextId++;
            incomes.Add(income);
            SaveData();
        }

        public bool EditIncome(int id, Income updatedIncome)
        {
            var income = incomes.FirstOrDefault(i => i.Id == id);
            if (income == null)
                return false;

            ValidateIncome(updatedIncome);
            ValidateDate(updatedIncome.Date);

            income.Amount = updatedIncome.Amount;
            income.Date = updatedIncome.Date;
            income.Description = updatedIncome.Description;
            income.Category = updatedIncome.Category;
            SaveData();
            return true;
        }
        public bool DeleteIncome(int id)
        {
            var income = incomes.FirstOrDefault(i => i.Id == id);
            if (income == null)
                return false;

            incomes.Remove(income);
            SaveData();
            return true;
        }

        public List<Income> GetAllIncomes()
        {
            return new List<Income>(incomes);
        }

        public Income GetIncomeById(int id)
        {
            return incomes.FirstOrDefault(i => i.Id == id);
        }
        public decimal GetTotalIncome()
        {
            return incomes.Sum(i => i.Amount);
        }
        public decimal GetTotalIncomeByDateRange(DateTime startDate, DateTime endDate)
        {
            return incomes
                .Where(i => i.Date >= startDate && i.Date <= endDate)
                .Sum(i => i.Amount);
        }
        public List<Income> GetIncomesByCategory(string category)
        {
            return incomes.Where(i => i.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private void ValidateIncome(Income income)
        {
            if (income.Amount <= 0)
            {
                throw new ArgumentException("Income amount must be greater than zero.");
            }
            if (string.IsNullOrWhiteSpace(income.Description))
            {
                throw new ArgumentException("Income description cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(income.Category))
            {
                throw new ArgumentException("Income category cannot be empty.");
            }
        }
        private void ValidateDate(DateTime date)
        {
            if (date > DateTime.Now)
            {
                throw new ArgumentException("Date cannot be in the future.");
            }
        }
    }
    public class ExpenseManager
    {
        private List<Expense> expenses = new List<Expense>();
        private int nextId;
        private DataService dataService;
        public ExpenseManager(DataService dataService)
        {
            this.dataService = dataService;
            LoadData();
        }
        private void LoadData()
        {
            expenses = dataService.LoadExpenses();
            var settings = dataService.LoadSettings();
            nextId = settings.NextExpenseId;

            if (expenses.Any())
            {
                int maxId = expenses.Max(e => e.Id);
                if (nextId <= maxId)
                    nextId = maxId + 1;
            }
        }
        private void SaveData()
        {
            dataService.SaveExpenses(expenses);

            var settings = dataService.LoadSettings();
            settings.NextExpenseId = nextId;
            dataService.SaveSettings(settings);
        }
        public void AddExpense(Expense expense)
        {
            ValidateExpense(expense);
            ValidateDate(expense.Date);

            expense.Id = nextId++;
            expenses.Add(expense);
            SaveData();
        }
        public bool EditExpense(int id, Expense updatedExpense)
        {
            var expense = expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null)
                return false;

            ValidateExpense(updatedExpense);
            ValidateDate(updatedExpense.Date);

            expense.Amount = updatedExpense.Amount;
            expense.Date = updatedExpense.Date;
            expense.Description = updatedExpense.Description;
            expense.Category = updatedExpense.Category;
            SaveData();
            return true;
        }
        public bool DeleteExpense(int id)
        {
            var expense = expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null)
                return false;

            expenses.Remove(expense);
            SaveData();
            return true;
        }
        public List<Expense> GetAllExpenses()
        {
            return new List<Expense>(expenses);
        }
        public Expense GetExpenseById(int id)
        {
            return expenses.FirstOrDefault(e => e.Id == id);
        }
        public decimal GetTotalExpenses()
        {
            return expenses.Sum(e => e.Amount);
        }
        public decimal GetTotalExpensesByDateRange(DateTime startDate, DateTime endDate)
        {
            return expenses
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .Sum(e => e.Amount);
        }
        public List<Expense> GetExpensesByCategory(string category)
        {
            return expenses.Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public Dictionary<string, decimal> GetExpenseBreakdownByCategory()
        {
            return expenses
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        }
        private void ValidateExpense(Expense expense)
        {
            if (expense.Amount <= 0)
            {
                throw new ArgumentException("Expense amount must be greater than zero.");
            }
            if (string.IsNullOrWhiteSpace(expense.Description))
            {
                throw new ArgumentException("Expense description cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(expense.Category))
            {
                throw new ArgumentException("Expense category cannot be empty.");
            }
        }
        private void ValidateDate(DateTime date)
        {
            if (date > DateTime.Now)
            {
                throw new ArgumentException("Date cannot be in the future.");
            }
        }
    }
    public class BudgetManager
    {
        private IncomeManager incomeManager;
        private ExpenseManager expenseManager;
        public BudgetManager()
        {
            DataService dataService = new DataService();
            incomeManager = new IncomeManager(dataService);
            expenseManager = new ExpenseManager(dataService);
        }
        public IncomeManager IncomeManager => incomeManager;
        public ExpenseManager ExpenseManager => expenseManager;
        public decimal GetNetBalance()
        {
            return incomeManager.GetTotalIncome() - expenseManager.GetTotalExpenses();
        }
        public decimal GetNetBalanceByDateRange(DateTime startDate, DateTime endDate)
        {
            decimal totalIncome = incomeManager.GetTotalIncomeByDateRange(startDate, endDate);
            decimal totalExpenses = expenseManager.GetTotalExpensesByDateRange(startDate, endDate);
            return totalIncome - totalExpenses;
        }
        public void DisplaySummary()
        {
            decimal totalIncome = incomeManager.GetTotalIncome();
            decimal totalExpenses = expenseManager.GetTotalExpenses();
            decimal netBalance = GetNetBalance();

            Console.WriteLine("Budget Summary");
            Console.WriteLine($"Total Income:   {totalIncome:N2:C}");
            Console.WriteLine($"Total Expenses: {totalExpenses:N2:C}");
            Console.WriteLine($"Net Balance:    {netBalance:N2:C}"); 
        }
    }
}