using Personal_Budget_Tracker;
using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            BudgetManager budgetManager = new BudgetManager();
            MenuManager menuManager = new MenuManager(budgetManager);
            menuManager.Run();
        }
    }
}