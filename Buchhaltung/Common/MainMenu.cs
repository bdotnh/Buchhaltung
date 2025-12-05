using System;

namespace Buchhaltung.Common
{
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
            Menu.ShowPrompts(promptMainMenu);
            int menuChoice = User.GetMenuChoice();
            if (menuChoice == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                if (menuChoice == 1)
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
                }
                else if (menuChoice == 2)
                {
                    _ = new Month();
                }
                else if (menuChoice == 3)
                {
                    _ = new FixCost();
                }
            }
        }
    }
}