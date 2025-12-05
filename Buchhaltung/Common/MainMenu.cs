using System;

namespace Buchhaltung.Common
{
    public enum EMenu
    {
        Exit = 0, NewEntry, Month, FixCosts
    }

    public class MainMenu
    {
        private string[] promptMainMenu =
        [
            "Exit",
            "Neuer Eintrag",
            "Monats-Übersicht",
            "Fix Kosten"
        ];

        public MainMenu()
        {
            bool onExit = false;
            while (!onExit)
            {
                Menu.ShowPrompts(promptMainMenu);
                string message = "Eingabe: ";
                int menuChoice = User.GetInputNumber(message);
                if (menuChoice == (int)EMenu.Exit)
                {
                    onExit = true;
                    Environment.Exit(0);
                }
                else
                {
                    if (menuChoice == (int)EMenu.NewEntry)
                    {
                        /* Dictionary<string, object> inputs = Entry.GetInputs();
                        Entry entry = new Entry(
                            (string)inputs.GetValueOrDefault("Datum"),
                            (float)inputs.GetValueOrDefault("Betrag"),
                            (string)inputs.GetValueOrDefault("Geschäft"),
                            (bool)inputs.GetValueOrDefault("IstAusgabe"),
                            (bool)inputs.GetValueOrDefault("IstFix")
                        );
                        */
                        string datum = "30.11.2025";
                        float betrag = 12.34f;
                        string geschäft = "Aldi";
                        bool istAusgabe = true;
                        bool istFix = true;

                        Entry testEntry = new Entry(
                            datum, betrag, geschäft, istAusgabe, istFix
                        );
                        Entry.Save(Common.currMonthFilepath, testEntry);
                        menuChoice = -1;
                    }
                    else if (menuChoice == (int)EMenu.Month)
                    {
                        _ = new Month();
                    }
                    else if (menuChoice == (int)EMenu.FixCosts)
                    {
                        _ = new FixCost();
                    }
                }
            }
        }
    }
}