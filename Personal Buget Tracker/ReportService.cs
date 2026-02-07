namespace Personal_Buget_Tracker
{
    public class ReportService
    {
        private readonly IncomeManager incomeManager;
        private readonly ExpenseManager expenseManager;
        public ReportService(BudgetManager budgetManager)
        {
            incomeManager = budgetManager.IncomeManager;
            expenseManager = budgetManager.ExpenseManager;
        }
        public record MonthlySummary(decimal TotalIncome, decimal TotalExpenses, decimal NetSavings);
        public MonthlySummary GetMonthlySummary(int month, int year)
        {
            var totalIncome = incomeManager.GetTotalIncomeByDateRange(
                new DateTime(year, month, 1),
                new DateTime(year, month, DateTime.DaysInMonth(year, month))
            );
            var totalExpenses = expenseManager.GetTotalExpensesByDateRange(
                new DateTime(year, month, 1),
                new DateTime(year, month, DateTime.DaysInMonth(year, month))
            );
            var netSavings = totalIncome - totalExpenses;
            return new MonthlySummary(totalIncome, totalExpenses, netSavings);
        }
        public record ExpenseCategorySummary(string Category, decimal Total);
        public List<ExpenseCategorySummary> GetExpenensesByCategory()
        {
            var expenses = expenseManager.GetAllExpenses();

            if (!expenses.Any())
                return new List<ExpenseCategorySummary>();

            return expenses
                .GroupBy(e => e.Category.ToString())
                .Select(g => new ExpenseCategorySummary(
                    g.Key,
                    g.Sum(e => e.Amount)
                ))
                .OrderByDescending(g => g.Total)
                .ToList();
        }
        public record ReportByDateRange (decimal TotalIncome, decimal TotalExpenses, decimal NetBalance);
        public ReportByDateRange GetReportByDateRange(DateTime startDate, DateTime endDate)
        {
            if(startDate > endDate)
                throw new ArgumentException("Start date must be earlier than or equal to end date.");

            var totalIncome = incomeManager.GetTotalIncomeByDateRange(startDate, endDate);
            var totalExpenses = expenseManager.GetTotalExpensesByDateRange(startDate, endDate);
            var netBalance = totalIncome - totalExpenses;
            return new ReportByDateRange(totalIncome, totalExpenses, netBalance);
        }
        public record TopSpendingCategory(string Category, decimal Total);
        public List<TopSpendingCategory> GetTopSpendingsCategories()
        {
            var expenses = expenseManager.GetAllExpenses();

            if (!expenses.Any())
                return new List<TopSpendingCategory>();

            return expenses
                .Where(i => i != null)
                .GroupBy(e => e.Category.ToString() ?? "Uncategorized")
                .Select(g => new TopSpendingCategory(
                     g.Key,
                     g.Sum(e => e.Amount)
                ))
                .OrderByDescending(g => g.Total)
                .ToList();
        }
        public decimal GetSavingsRate()
        {
            var totalIncome = incomeManager.GetAllIncomes().Sum(i => i.Amount);
            var totalExpenses = expenseManager.GetAllExpenses().Sum(e => e.Amount);
            if (totalIncome == 0)
            {
                return 0;
            }
            var savings = totalIncome - totalExpenses;
            var savingsRate = savings / totalIncome * 100;
            return savingsRate;
        }
        public record IncomeByCategory(string Category, decimal Total, decimal Percentage);
        public List<IncomeByCategory> GetIncomesByCategory()
        {
            var incomes = incomeManager.GetAllIncomes();

            if (!incomes.Any())
            {
                return new List<IncomeByCategory>();
            }

            var totalIncome = incomes.Sum(i => i.Amount);

            return incomes
                .Where(i => i != null)
                .GroupBy(i => i.Category?.ToString() ?? "Uncategorized")
                .Select(g => new IncomeByCategory(
                    g.Key,
                    g.Sum(i => i.Amount),
                    g.Sum(i => i.Amount) / totalIncome * 100
                ))
                .OrderByDescending(g => g.Total)
                .ToList();
        }
        public record YearToDateSummary(decimal TotalIncome, decimal TotalExpenses, decimal NetBalance);
        public YearToDateSummary GetYearToDateSummary()
        {
            var startOfYear = new DateTime(DateTime.Now.Year, 1, 1);
            var today = DateTime.Now;

            var totalIncome = incomeManager.GetTotalIncomeByDateRange(startOfYear, today);
            var totalExpenses = expenseManager.GetTotalExpensesByDateRange(startOfYear, today);
            var netBalance = totalIncome - totalExpenses;

            return new YearToDateSummary(totalIncome, totalExpenses, netBalance);
        }
    }
}
