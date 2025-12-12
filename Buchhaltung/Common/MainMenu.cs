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
                Menu.ShowPrompts(promptMainMenu);
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
                        /*
                        string datum = """30.11.2025""";
                        float betrag = 12.34f;
                        string geschäft = """Aldi""";
                        bool istAusgabe = true;
                        bool istFix = true;
                        Entry testEntry = new Entry(
                            datum, betrag, geschäft, istAusgabe, istFix
                        );
                        Entry.Save(Common.currMonthFilepath, testEntry);
                        */
                        Entry.Save(Common.CurrMonthFilepath, entry);
                    }
                    if (menuChoice == (int)EMenu.Month)
                    {
                        new Month("").Show();
                        new Menu((int)EMenu.Month);
                    }
                    else if (menuChoice == (int)EMenu.FixCosts)
                    {
                        new FixCost().Show();
                        new Menu((int)EMenu.FixCosts);
                    }
                    else if (menuChoice == (int)EMenu.Year)
                    {
                        new Year("").ShowAllMonthTotals();
                        new Menu((int)EMenu.Year);
                    }
                    else if (menuChoice == (int)EMenu.MonthComparison)
                    {
                        // string m1 = User.GetMonthInput("Monat 1 eingeben: ");
                        // string m2 = User.GetMonthInput("Monat 2 eingeben: ");
                        string monthDate1 = "11.2025";
                        string monthDate2 = "12.2025";

                        MonthComparison monthComparison = new(monthDate1, monthDate2);
                        monthComparison.ShowTotalDiffs();
                        new Menu((int)EMenu.MonthComparison);
                    }
                    else if (menuChoice == (int)EMenu.YearComparison)
                    {
                        // string y1 = User.GetYearInput("Jahr 1 eingeben: ");
                        // string y2 = User.GetYearInput("Jahr 2 eingeben: ");
                        string yearDate1 = "2024";
                        string yearDate2 = "2025";

                        YearComparison yearComparison = new(yearDate1, yearDate2);
                        yearComparison.ShowTotalDiffs();
                    }
                }
            }
        }
    }
}