using Personal_Budget_Tracker;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Personal_Budget_Tracker
{
    public class InputHelper
    {
        public static int GetIntInput(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= min && result <= max) return result;
                    else
                        Console.WriteLine($"Please enter a number between {min} and {max}.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }
        public static decimal GetDecimalInput(string prompt, decimal min = 0)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal result))
                {
                    if (result > min) return result;
                    else
                        Console.WriteLine($"Amount must be greater than {min}.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid amount.");
                }
            }
        }
        public static DateTime GetDateInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                {
                    if (result <= DateTime.Now)
                        return result;
                    else
                        Console.WriteLine("Date cannot be in the future.");
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please use format: yyyy-mm-dd or mm/dd/yyyy");
                }
            }
        }
        public static string GetStringInput(string prompt, bool allowEmpty = false)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) || allowEmpty)
                    return input?.Trim() ?? "";
                else
                    Console.WriteLine("Input cannot be empty.");
            }
        }
        public static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
    public class MenuManager
    {
        private BudgetManager budgetManager;
        private ReportService reportService;
        private bool isRunning;

        private readonly string[] incomeCategories = { "Salary", "Freelance", "Investment", "Gift", "Other" };
        private readonly string[] expenseCategories = { "Food", "Rent", "Utilities", "Transportation", "Entertainment", "Shopping", "Healthcare", "Other" };
        public MenuManager(BudgetManager manager)
        {
            budgetManager = manager;
            reportService = new ReportService(budgetManager.IncomeManager, budgetManager.ExpenseManager);
            isRunning = true;
        }
        public void Run()
        {
            while (isRunning)
            {
                ShowMainMenu();
            }
        }

        private void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Personal budget tracker");
            Console.WriteLine("1. Add income");
            Console.WriteLine("2. Add expense");
            Console.WriteLine("3. View all incomes");
            Console.WriteLine("4. View all expenses");
            Console.WriteLine("5. Edit transaction");
            Console.WriteLine("6. Delete transaction");
            Console.WriteLine("7. Reports & summaries");
            Console.WriteLine("8. Exit");

            int choice = InputHelper.GetIntInput("Select an option (1-8): ", 1, 8);

            switch (choice)
            {
                case 1:
                    ShowAddIncomeMenu();
                    break;
                case 2:
                    ShowAddExpenseMenu();
                    break;
                case 3:
                    ShowViewIncomesMenu();
                    break;
                case 4:
                    ShowViewExpensesMenu();
                    break;
                case 5:
                    ShowEditTransactionMenu();
                    break;
                case 6:
                    ShowDeleteTransactionMenu();
                    break;
                case 7:
                    ShowReportsMenu();
                    break;
                case 8:
                    isRunning = false;
                    Console.Clear();
                    break;
                default:
                    break;
            }
        }
        private void ShowAddIncomeMenu()
        {
            Console.Clear();
            Console.WriteLine("Add income");

            try
            {
                decimal amount = InputHelper.GetDecimalInput("Enter amount: EUR ");
                DateTime date = InputHelper.GetDateInput("Enter date (yyyy-mm-dd): ");
                string description = InputHelper.GetStringInput("Enter description: ");
                string category = GetCategoryChoice(incomeCategories, "income");

                Income income = new Income
                {
                    Amount = amount,
                    Date = date,
                    Description = description,
                    Category = category
                };

                budgetManager.IncomeManager.AddIncome(income);

                Console.WriteLine($"Income of EUR {amount:N2} added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            InputHelper.PressAnyKey();
        }
        private void ShowAddExpenseMenu()
        {
            Console.Clear();
            Console.WriteLine("Add expanse");

            try
            {
                decimal amount = InputHelper.GetDecimalInput("Enter amount: EUR");
                DateTime date = InputHelper.GetDateInput("Enter date (yyyy-mm-dd): ");
                string description = InputHelper.GetStringInput("Enter description: ");
                string category = GetCategoryChoice(expenseCategories, "expense");

                Expense expense = new Expense
                {
                    Amount = amount,
                    Date = date,
                    Description = description,
                    Category = category
                };

                budgetManager.ExpenseManager.AddExpense(expense);

                Console.WriteLine($"Expense of ${amount:N2} added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            InputHelper.PressAnyKey();
        }
        private string GetCategoryChoice(string[] categories, string type)
        {
            Console.WriteLine();
            Console.WriteLine($"Select {type} category:");
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i]}");
            }

            int choice = InputHelper.GetIntInput($"Choice (1-{categories.Length}): ", 1, categories.Length);
            return categories[choice - 1];
        }
        private void ShowViewIncomesMenu()
        {
            Console.Clear();
            Console.WriteLine("All incomes");

            var incomes = budgetManager.IncomeManager.GetAllIncomes();

            if (!incomes.Any())
            {
                Console.WriteLine("No incomes recorded yet.");
            }
            else
            {
                Console.WriteLine($"{"ID",-5} {"Date",-12} {"Amount",-12} {"Category",-15} {"Description",-30}");
                Console.WriteLine(new string('─', 79));

                foreach (var income in incomes.OrderByDescending(i => i.Date))
                {
                    Console.WriteLine($"{income.Id,-5} {income.Date:yyyy-MM-dd,-12} {income.Amount,-12:C} {income.Category,-15} {income.Description,-30}");
                }

                Console.WriteLine(new string('─', 79));
                Console.WriteLine($"Total Income: {incomes.Sum(i => i.Amount):C}");
            }
            InputHelper.PressAnyKey();
        }
        private void ShowViewExpensesMenu()
        {
            Console.Clear();
            Console.WriteLine("All expenses");

            var expenses = budgetManager.ExpenseManager.GetAllExpenses();

            if (!expenses.Any())
            {
                Console.WriteLine("No expenses recorded yet.");
            }
            else
            {
                Console.WriteLine($"{"ID",-5} {"Date",-12} {"Amount",-12} {"Category",-15} {"Description",-30}");
                Console.WriteLine(new string('─', 79));

                foreach (var expense in expenses.OrderByDescending(e => e.Date))
                {
                    Console.WriteLine($"{expense.Id,-5} {expense.Date:yyyy-MM-dd,-12} {expense.Amount,-12:C} {expense.Category,-15} {expense.Description,-30}");
                }

                Console.WriteLine(new string('─', 79));
                Console.WriteLine($"Total Expenses: {expenses.Sum(e => e.Amount):C}");
            }
            InputHelper.PressAnyKey();
        }
        private void ShowEditTransactionMenu()
        {
            Console.Clear();
            Console.WriteLine("Edit transaction");
            Console.WriteLine("1. Edit income");
            Console.WriteLine("2. Edit expense");
            Console.WriteLine("3. Back to menu");

            int choice = InputHelper.GetIntInput("Select an option (1-3): ", 1, 3);

            if (choice == 1)
                EditIncome();
            else if (choice == 2)
                EditExpense();
            else if (choice == 3)
                return;
        }
        private void EditIncome()
        {
            Console.Clear();
            var incomes = budgetManager.IncomeManager.GetAllIncomes();

            if (!incomes.Any())
            {
                Console.WriteLine("No incomes to edit.");
                InputHelper.PressAnyKey();
                return;
            }

            ShowViewIncomesMenu();
            Console.WriteLine();

            int id = InputHelper.GetIntInput("Enter income ID to edit (0 to cancel): ");
            if (id == 0) return;

            var income = budgetManager.IncomeManager.GetIncomeById(id);
            if (income == null)
            {
                Console.WriteLine("Income not found.");
                InputHelper.PressAnyKey();
                return;
            }

            Console.WriteLine($"Editing income ID {id}");
            Console.WriteLine($"Current: ${income.Amount:N2} - {income.Description}");

            decimal amount = InputHelper.GetDecimalInput($"Enter new amount (current: ${income.Amount:N2}): $");
            DateTime date = InputHelper.GetDateInput($"Enter new date (current: {income.Date:yyyy-MM-dd}): ");
            string description = InputHelper.GetStringInput($"Enter new description (current: {income.Description}): ");
            string category = GetCategoryChoice(incomeCategories, "income");

            Income updatedIncome = new Income
            {
                Amount = amount,
                Date = date,
                Description = description,
                Category = category
            };

            if (budgetManager.IncomeManager.EditIncome(id, updatedIncome))
                Console.WriteLine(" Income updated successfully!");
            else
                Console.WriteLine("Failed to update income.");

            InputHelper.PressAnyKey();
        }
        private void EditExpense()
        {
            Console.Clear();
            var expenses = budgetManager.ExpenseManager.GetAllExpenses();

            if (!expenses.Any())
            {
                Console.WriteLine("No expenses to edit.");
                InputHelper.PressAnyKey();
                return;
            }

            ShowViewExpensesMenu();
            Console.WriteLine();

            int id = InputHelper.GetIntInput("Enter expense ID to edit (0 to cancel): ");
            if (id == 0) return;

            var expense = budgetManager.ExpenseManager.GetExpenseById(id);
            if (expense == null)
            {
                Console.WriteLine("Expense not found.");
                InputHelper.PressAnyKey();
                return;
            }

            Console.WriteLine($"Editing Expense ID {id}");
            Console.WriteLine($"Current: ${expense.Amount:N2} - {expense.Description}");

            decimal amount = InputHelper.GetDecimalInput($"Enter new amount (current: ${expense.Amount:N2}): $");
            DateTime date = InputHelper.GetDateInput($"Enter new date (current: {expense.Date:yyyy-MM-dd}): ");
            string description = InputHelper.GetStringInput($"Enter new description (current: {expense.Description}): ");
            string category = GetCategoryChoice(expenseCategories, "expense");

            Expense updatedExpense = new Expense
            {
                Amount = amount,
                Date = date,
                Description = description,
                Category = category
            };

            if (budgetManager.ExpenseManager.EditExpense(id, updatedExpense))
                Console.WriteLine("Expense updated successfully!");
            else
                Console.WriteLine("Failed to update expense.");

            InputHelper.PressAnyKey();
        }
        private void ShowDeleteTransactionMenu()
        {
            Console.Clear();
            Console.WriteLine("Delete transaction");
            Console.WriteLine();
            Console.WriteLine("1. Delete Income");
            Console.WriteLine("2. Delete Expense");
            Console.WriteLine("3. Back to Main Menu");

            int choice = InputHelper.GetIntInput("Select an option (1-3): ", 1, 3);

            if (choice == 1)
                DeleteIncome();
            else if (choice == 2)
                DeleteExpense();
        }
        private void DeleteIncome()
        {
            Console.Clear();
            var incomes = budgetManager.IncomeManager.GetAllIncomes();

            if (!incomes.Any())
            {
                Console.WriteLine("No incomes to delete.");
                InputHelper.PressAnyKey();
                return;
            }

            ShowViewIncomesMenu();

            int id = InputHelper.GetIntInput("Enter income ID to delete (0 to cancel): ");
            if (id == 0) return;

            var income = budgetManager.IncomeManager.GetIncomeById(id);
            if (income == null)
            {
                Console.WriteLine("Income not found.");
                InputHelper.PressAnyKey();
                return;
            }

            Console.WriteLine($"Are you sure you want to delete:");
            Console.WriteLine($"${income.Amount:N2} - {income.Description} ({income.Date:yyyy-MM-dd})");
            Console.Write("Type 'yes' to confirm:");

            if (Console.ReadLine()?.ToLower() == "yes")
                if (budgetManager.IncomeManager.DeleteIncome(id))
                    Console.WriteLine("Income deleted successfully!");
                else
                    Console.WriteLine("Deletion cancelled.");

            InputHelper.PressAnyKey();
        }
        private void DeleteExpense()
        {
            Console.Clear();
            var expenses = budgetManager.ExpenseManager.GetAllExpenses();

            if (!expenses.Any())
            {
                Console.WriteLine("No expenses to delete.");
                InputHelper.PressAnyKey();
                return;
            }

            ShowViewExpensesMenu();
            Console.WriteLine();

            int id = InputHelper.GetIntInput("Enter expense ID to delete (0 to cancel): ");
            if (id == 0) return;

            var expense = budgetManager.ExpenseManager.GetExpenseById(id);
            if (expense == null)
            {
                Console.WriteLine("Expense not found.");
                InputHelper.PressAnyKey();
                return;
            }

            Console.WriteLine($"Are you sure you want to delete:");
            Console.WriteLine($"${expense.Amount:N2} - {expense.Description} ({expense.Date:yyyy-MM-dd})");
            Console.Write("Type 'yes' to confirm: ");

            if (Console.ReadLine()?.ToLower() == "yes")
                if (budgetManager.ExpenseManager.DeleteExpense(id))
                    Console.WriteLine("Expense deleted successfully!");
                else
                    Console.WriteLine("Deletion cancelled.");

            InputHelper.PressAnyKey();
        }
        private void ShowReportsMenu()
        {
            bool inReportsMenu = true;

            while (inReportsMenu)
            {
                Console.Clear();
                Console.WriteLine("Reports & summaries");
                Console.WriteLine();
                Console.WriteLine("1. Monthly summary");
                Console.WriteLine("2. Expense breakdown by category");
                Console.WriteLine("3. Income breakdown by category");
                Console.WriteLine("4. Date range report");
                Console.WriteLine("5. Top spending categories");
                Console.WriteLine("6. Savings rate");
                Console.WriteLine("7. Year to date summary");
                Console.WriteLine("8. Overall budget summary");
                Console.WriteLine("9. Back to main menu");

                int choice = InputHelper.GetIntInput("Select an option (1-9): ", 1, 9);

                switch (choice)
                {
                    case 1:
                        ShowMonthlySummaryReport();
                        break;
                    case 2:
                        reportService.DisplayExpensesByCategory();
                        InputHelper.PressAnyKey();
                        break;
                    case 3:
                        reportService.DisplayIncomesByCategory();
                        InputHelper.PressAnyKey();
                        break;
                    case 4:
                        ShowDateRangeReport();
                        break;
                    case 5:
                        ShowTopSpendingReport();
                        break;
                    case 6:
                        reportService.DisplaySavingsRate();
                        InputHelper.PressAnyKey();
                        break;
                    case 7:
                        reportService.DisplayYearToDateSummary();
                        InputHelper.PressAnyKey();
                        break;
                    case 8:
                        budgetManager.DisplaySummary();
                        InputHelper.PressAnyKey();
                        break;
                    case 9:
                        inReportsMenu = false;
                        break;
                    default:
                        break;
                }
            }
        }
        private void ShowMonthlySummaryReport()
        {
            Console.Clear();
            int month = InputHelper.GetIntInput("Enter month (1-12): ", 1, 12);
            int year = InputHelper.GetIntInput("Enter year: ", 1900, DateTime.Now.Year + 10);

            reportService.DisplayMonthlySummary(month, year);
            InputHelper.PressAnyKey();
        }
        private void ShowDateRangeReport()
        {
            Console.Clear();
            DateTime startDate = InputHelper.GetDateInput("Enter start date (yyyy-mm-dd): ");
            DateTime endDate = InputHelper.GetDateInput("Enter end date (yyyy-mm-dd): ");

            reportService.DisplayReportByDateRange(startDate, endDate);
            InputHelper.PressAnyKey();
        }
        private void ShowTopSpendingReport()
        {
            Console.Clear();
            int count = InputHelper.GetIntInput("How many top categories to show? ", 1, 20);

            reportService.DisplayTopSpendingsCategories(count);
            InputHelper.PressAnyKey();
        }
    }
}