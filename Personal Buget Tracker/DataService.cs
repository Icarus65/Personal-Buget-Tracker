using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Personal_Buget_Tracker
{
    public class DataService
    {
        private readonly string dataFolder;
        private readonly string incomesFile;
        private readonly string expensesFile;
        private readonly string settingsFile;

        public DataService(string folderPath = "BudgetData")
        {
            dataFolder = folderPath;
            incomesFile = Path.Combine(dataFolder, "incomes.json");
            expensesFile = Path.Combine(dataFolder, "expenses.json");
            settingsFile = Path.Combine(dataFolder, "settings.json");

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
        }
        public void SaveIncomes(List<Income> incomes)
        {
            try
            {
                string json = JsonConvert.SerializeObject(incomes, Formatting.Indented);
                File.WriteAllText(incomesFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving incomes: {ex.Message}");
            }
        }
        public List<Income> LoadIncomes()
        {
            try
            {
                if (!File.Exists(incomesFile)) return new List<Income>();

                string json = File.ReadAllText(incomesFile);
                return JsonConvert.DeserializeObject<List<Income>>(json) ?? new List<Income>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading incomes: {ex.Message}");
                return new List<Income>();
            }
        }
        public void SaveExpenses(List<Expense> expenses)
        {
            try
            {
                string json = JsonConvert.SerializeObject(expenses, Formatting.Indented);
                File.WriteAllText(expensesFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving expenses: {ex.Message}");
            }
        }
        public List<Expense> LoadExpenses()
        {
            try
            {
                if (!File.Exists(expensesFile))
                    return new List<Expense>();

                string json = File.ReadAllText(expensesFile);
                return JsonConvert.DeserializeObject<List<Expense>>(json) ?? new List<Expense>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading expenses: {ex.Message}");
                return new List<Expense>();
            }
        }
        public void SaveSettings(AppSettings settings)
        {
            try
            {
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(settingsFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving settings: {ex.Message}");
            }
        }
        public AppSettings LoadSettings()
        {
            try
            {
                if (!File.Exists(settingsFile))
                    return new AppSettings();

                string json = File.ReadAllText(settingsFile);
                return JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading settings: {ex.Message}");
                return new AppSettings();
            }
        }
    }
    public class AppSettings
    {
        public int NextIncomeId { get; set; } = 1;
        public int NextExpenseId { get; set; } = 1;
    }
}