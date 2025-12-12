using System;

namespace Buchhaltung.Common
{
    public enum EMonthMenu
    {
        Exit = 0,
        ChangeMonth,
        DeleteEntries,
    }
    public enum EFixMenu
    {
        Exit = 0,
        DeleteEntry,
    }

    public class Menu
    {
        public static string[] menuPrompts = [];

        private string[] promptEntryMenu =
        [
            "Exit"
        ];

        private string[] promptMonthMenu =
        [
            "Exit", "Monat ändern", "Einträge löschen"
        ];

        private string[] promptFixMenu =
        [
            "Exit", "Fixkosten-Einträge löschen"
        ];

        private string[] promptMonthComparisonMenu =
        [
            "Exit",
        ];

        public Menu(int menu)
        {
            InitMenuPrompt(menu);
            if (menu == (int)EMenu.Month)
            {
                MonthMenu();
            }
            if (menu == (int)EMenu.FixCosts)
            {
                FixCostMenu();
            }
        }

        private static void MonthMenu()
        {
            Month month = new("");
            string newDate = "";
            string message = "Eingabe: ";
            int menuChoice = User.GetInputNumber(message);
            if (menuChoice == (int)EMonthMenu.Exit)
            {
                _ = new MainMenu();
            }
            else
            {
                if (menuChoice == (int)EMonthMenu.ChangeMonth)
                {
                    newDate = User.GetMonthInput("Monat: ");
                    new Month(newDate).ShowEntriesAndTotals();
                }
                else if (menuChoice == (int)EMonthMenu.DeleteEntries)
                {
                    month = new Month(newDate);
                    List<int> entryChoices = User.GetNumsInput("Zum löschen mehrerer Einträge Nummer mit ',' trennen: ");
                    List<Entry> entries = month.SelectEntries(entryChoices);
                    month.DeleteEntries(entries);
                }
            }
        }

        private static void FixCostMenu()
        {
            FixCost fixCost = new();
            string message = "Eingabe: ";
            int menuChoice = User.GetInputNumber(message);
            if (menuChoice == (int)EFixMenu.Exit)
            {
                _ = new MainMenu();
            }
            else
            {
                if (menuChoice == (int)EFixMenu.DeleteEntry)
                {

                    List<int> entryChoices = User.GetNumsInput("Zum löschen mehrerer Einträge Nummer mit ',' trennen: ");
                    List<Entry> entries = fixCost.SelectEntries(entryChoices);
                    fixCost.DeleteEntries(entries); 
                }
            }
        }

        private static int GetFixCostChoice()
        {
            string message = "Zum löschen des Fixkosten-Eintrages, bitte Nummer eingeben: ";
            int userChoice = User.GetInputNumber(message);

            return userChoice;
        }

        public static void ShowPrompts(string[] menuPrompts)
        {
            Console.WriteLine("-- HauptMenü --");
            for (int i = 0; i < menuPrompts.Length; i++)
            {
                Console.WriteLine($"{i}. {menuPrompts[i]}.");
            }
        }

        private void InitMenuPrompt(int menu)
        {
            if (menu == (int)EMenu.NewEntry)
            {
                ShowPrompts(promptEntryMenu);
            }
            else if (menu == (int)EMenu.Month)
            {
                ShowPrompts(promptMonthMenu);
            }
            else if (menu == (int)EMenu.FixCosts)
            {
                ShowPrompts(promptFixMenu);
            }
            else if (menu == (int)EMenu.MonthComparison)
            {
                ShowPrompts(promptMonthComparisonMenu);
            }
        }
    }
}
