using System;

namespace Buchhaltung.Common
{
    public enum EMenu
    {
        Exit = 0,
        NewEntry,
        Month,
        Year,
        FixCosts,
        MonthComparison,
        YearComparison
    }

    public class MainMenu
    {
        private string[] promptMainMenu =
        [
            """Exit""",
            """Neue Einträge""",
            """Monats-Übersicht""",
            """Jahres-Übersicht""",
            """Fix Kosten""",
            """Monats-Vergleich""",
            """Jahres-Vergleich"""
        ];

        public MainMenu()
        {
            bool onExit = false;
            while (!onExit)
            {
                ShowMainMenuPrompts();
                string message = """Eingabe: """;
                int menuChoice = User.GetInputNumber(message);
                if (menuChoice == (int)EMenu.Exit)
                {
                    onExit = true;
                    Environment.Exit(0);
                }
                else
                {
                    while (menuChoice == (int)EMenu.NewEntry)
                    {
                        Dictionary<string, object> inputs = Entry.GetInputs();
                        Entry entry = new Entry(
                            (string)inputs.GetValueOrDefault("Datum"),
                            (float)inputs.GetValueOrDefault("Betrag"),
                            (string)inputs.GetValueOrDefault("Geschäft"),
                            (bool)inputs.GetValueOrDefault("IstAusgabe"),
                            (bool)inputs.GetValueOrDefault("IstFix")
                        );
                        string monthInput = entry.Datum.Substring(3);
                        DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
                        string thisMonth = $"{dateNow.Month}.{dateNow.Year}";
                        string filepath;
                        if (monthInput == thisMonth)
                        {
                            filepath = Common.CurrMonthFilepath;
                        }
                        else
                        {
                            filepath = Common.GetFilepathFromMonthDate(monthInput);
                        }
                        Entry.Save(filepath, entry);
                    }
                    if (menuChoice == (int)EMenu.Month)
                    {
                        Month month = new("");
                        month.Show();
                        new Menu((int)EMenu.Month, month, null);
                    }
                    else if (menuChoice == (int)EMenu.Year)
                    {
                        Year year = new("");
                        year.Show();
                        new Menu((int)EMenu.Year, null, year);
                    }
                    else if (menuChoice == (int)EMenu.FixCosts)
                    {
                        if (FixCost.fixCount < 1)
                        {
                            Console.WriteLine("Es sind noch keine Fixkosten vorhanden.");
                        }
                        else
                        {
                            new FixCost().Show();
                            new Menu((int)EMenu.FixCosts);
                        }
                    }
                    else if (menuChoice == (int)EMenu.MonthComparison)
                    {
                        new Menu((int)EMenu.MonthComparison);
                    }
                    else if (menuChoice == (int)EMenu.YearComparison)
                    {

                        new Menu((int)EMenu.YearComparison);
                    }
                }
            }
        }
        private void ShowMainMenuPrompts()
        {
            Console.WriteLine("\n-- Hauptmenü --");
            for (int i = 0; i < promptMainMenu.Length; i++)
            {
                Console.WriteLine($"{i}. {promptMainMenu[i]}.");
            }
        }
    }
}