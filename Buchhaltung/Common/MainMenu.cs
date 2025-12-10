using System;
using System.Collections.Generic;
using Utf8Json.Formatters;

namespace Buchhaltung.Common
{
    public enum EMenu
    {
        Exit = 0, NewEntry, Month, FixCosts, MonthComparison
    }

    public class MainMenu
    {
        private string[] promptMainMenu =
        [
            """Exit""",
            """Neue Einträge""",
            """Monats-Übersicht""",
            """Fix Kosten""",
            """Monats-Vergleich"""
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
                    else if (menuChoice == (int)EMenu.MonthComparison)
                    {
                        // string m1 = User.GetMonthInput("Monat 1 eingeben: ");
                        // string m2 = User.GetMonthInput("Monat 2 eingeben: ");
                        string monthDate1 = "11.2025";
                        string monthDate2 = "12.2025";
                       
                        MonthComparison monthComparison = new (monthDate1, monthDate2); 
                        monthComparison.ShowDiffs();
                        new Menu((int)EMenu.MonthComparison);
                    }
                }
            }
        }
    }
}