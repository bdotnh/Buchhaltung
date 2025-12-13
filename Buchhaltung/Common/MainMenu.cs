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
                        Entry.Save(Common.CurrMonthFilepath, entry);
                    }
                    if (menuChoice == (int)EMenu.Month)
                    {
                        new Month("").Show();
                        new Menu((int)EMenu.Month);
                    }
                    else if (menuChoice == (int)EMenu.Year)
                    {
                        new Year("").Show();
                        new Menu((int)EMenu.Year);
                    }
                    else if (menuChoice == (int)EMenu.FixCosts)
                    {
                        new FixCost().Show();
                        if (FixCost.fixCount > 1)
                        {
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
            Console.WriteLine("-- Hauptmenü --");
            for (int i = 0; i < promptMainMenu.Length; i++)
            {
                Console.WriteLine($"{i}. {promptMainMenu[i]}.");
            }
        }
    }
}