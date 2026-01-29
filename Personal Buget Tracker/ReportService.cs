using Personal_Budget_Tracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Budget_Tracker
{
    public class ReportService
    {
        private IncomeManager incomeManager;
        private ExpenseManager expenseManager;
        public ReportService(IncomeManager incomeMgr, ExpenseManager expenseMgr)
        {
            incomeManager = incomeMgr;
            expenseManager = expenseMgr;
        }
        public void DisplayMonthlySummary(int month, int year)
        {
            if(month < 1 || month > 12)
                throw new ArgumentException("Invalid month. Please enter a value between 1 and 12.");
            if(year < 1900 || year > DateTime.Now.Year)
                throw new ArgumentException("Invalid year. Please enter a valid year.");

            var totalIncome = incomeManager.GetTotalIncomeByDateRange(
                new DateTime(year, month, 1),
                new DateTime(year, month, DateTime.DaysInMonth(year, month))
            );
            var totalExpenses = expenseManager.GetTotalExpensesByDateRange(
                new DateTime(year, month, 1),
                new DateTime(year, month, DateTime.DaysInMonth(year, month))
            );
            var netSavings = totalIncome - totalExpenses;
            Console.WriteLine($"Monthly Summary for {month}/{year}:");
            Console.WriteLine($"Total Income:       {totalIncome} EUR");
            Console.WriteLine($"Total Expenses:     {totalExpenses} EUR");
            Console.WriteLine($"Net Savings:        {netSavings} EUR");
        }
        public void DisplayExpensesByCategory()
        {
            var expenses = expenseManager.GetAllExpenses();

            if (!expenses.Any())
            {
                Console.WriteLine("No expenses recorded");
                return;
            }            

            var groupedExpenses = expenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(e => e.Amount)
                })
                .OrderByDescending(g => g.Total);
            Console.WriteLine("Expenses by Category:");
            foreach (var group in groupedExpenses)
            {
                Console.WriteLine($"{group.Category}: {group.Total} EUR");
            }
        }
        public void DisplayReportByDateRange(DateTime startDate, DateTime endDate)
        {
            if(startDate > endDate)
                throw new ArgumentException("Start date must be earlier than or equal to end date.");

            var totalIncome = incomeManager.GetTotalIncomeByDateRange(startDate, endDate);
            var totalExpenses = expenseManager.GetTotalExpensesByDateRange(startDate, endDate);
            var netBalance = totalIncome - totalExpenses;
            Console.WriteLine($"Report from     {startDate:d} to {endDate:d}:");
            Console.WriteLine($"Total Income:   {totalIncome} EUR");
            Console.WriteLine($"Total Expenses: {totalExpenses} EUR");
            Console.WriteLine($"Net Balance:    {netBalance} EUR");
        }
        public void DisplayTopSpendingsCategories(int count)
        {
            if(count <= 0)
                throw new ArgumentException("Count must be a positive integer.");

            var expenses = expenseManager.GetAllExpenses();

            if (!expenses.Any())
            {
                Console.WriteLine("No expenses recorded");
                return;
            }
            var topCategories = expenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(e => e.Amount)
                })
                .OrderByDescending(g => g.Total)
                .Take(count);
            Console.WriteLine($"Top {count} Spending Categories:");
            foreach (var category in topCategories)
            {
                Console.WriteLine($"{category.Category}: {category.Total} ");
            }
        }
        public void DisplaySavingsRate()
        {
            var totalIncome = incomeManager.GetAllIncomes().Sum(i => i.Amount);
            var totalExpenses = expenseManager.GetAllExpenses().Sum(e => e.Amount);
            if (totalIncome == 0)
            {
                Console.WriteLine("Savings Rate: N/A (No income recorded)");
                return;
            }
            var savings = totalIncome - totalExpenses;
            var savingsRate = (savings / totalIncome) * 100;
            Console.WriteLine($"Savings Rate: {savingsRate:F2}%");
        }
        public void DisplayIncomesByCategory()
        {
            var incomes = incomeManager.GetAllIncomes();

            if (!incomes.Any())
            {
                Console.WriteLine("No income recorded.");
                return;
            }

            var totalIncome = incomes.Sum(i => i.Amount);

            var groupedIncomes = incomes
                .GroupBy(i => i.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(i => i.Amount),
                    Percentage = (g.Sum(i => i.Amount) / totalIncome) * 100
                })
                .OrderByDescending(g => g.Total);

            Console.WriteLine("Income by Category");
            foreach (var group in groupedIncomes)
            {
                Console.WriteLine($"{group.Category.PadRight(20)} {group.Total,10} EUR  ({group.Percentage,5:F1}%)");
            }
        }
        public void DisplayYearToDateSummary()
        {
            var startOfYear = new DateTime(DateTime.Now.Year, 1, 1);
            var today = DateTime.Now;

            var totalIncome = incomeManager.GetTotalIncomeByDateRange(startOfYear, today);
            var totalExpenses = expenseManager.GetTotalExpensesByDateRange(startOfYear, today);
            var netBalance = totalIncome - totalExpenses;

            Console.WriteLine($"Year-to-Date Summary ({startOfYear.Year})");
            Console.WriteLine($"Total Income:    {totalIncome,12} EUR");
            Console.WriteLine($"Total Expenses:  {totalExpenses,12} EUR");
            Console.WriteLine($"Net Balance:     {netBalance,12} EUR");
        }
    }
}
