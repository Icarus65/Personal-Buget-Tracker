namespace Personal_Buget_Tracker
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

        public void UpdateIncome(Income income)
        {
            var existing = incomes.FirstOrDefault(i => i.Id == income.Id);
            if (existing != null)
            {
                existing.Description = income.Description;
                existing.Amount = income.Amount;
                existing.Date = income.Date;
                existing.Category = income.Category;
            }
            SaveData();
        }
        public void DeleteIncome(int id)
        {
            var income = incomes.FirstOrDefault(i => i.Id == id);
            if (income != null)
                incomes.Remove(income);
            SaveData();
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
            if (string.IsNullOrWhiteSpace(income.Category.ToString()))
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
            SaveData();
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
        public void UpdateExpense(Expense expense)
        {
            var existing = expenses.FirstOrDefault(e => e.Id == expense.Id);
            if (existing != null)
            {
                existing.Description = expense.Description;
                existing.Amount = expense.Amount;
                existing.Date = expense.Date;
                existing.Category = expense.Category;
            }
            SaveData();
        }
        public void DeleteExpense(int id)
        {
            var expense = expenses.FirstOrDefault(e => e.Id == id);
            if (expense != null)
                expenses.Remove(expense);
            SaveData();
        }
        public List<Expense> GetAllExpenses()
        {
            return new List<Expense>(expenses);
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
            return expenses.Where(e => e.Category.ToString().Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public Dictionary<string, decimal> GetExpenseBreakdownByCategory()
        {
            return expenses
                .GroupBy(e => e.Category.ToString())
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
            if (string.IsNullOrWhiteSpace(expense.Category.ToString()))
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
        private IncomeManager incomeManager { get; }
        private ExpenseManager expenseManager { get; }
        public static class CategoryRepository
        {
            public static readonly string[] IncomeCategories = { "Salary", "Gift", "Investment", "Other" };
            public static readonly string[] ExpenseCategories = { "Food", "Rent", "Utilities", "Entertainment", "Other" };
        }
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

        }
    }
}