using System;

namespace Buchhaltung.Common
{
    public enum EMonthMenu
    {
        Exit = 0,
          ChangeMonth,
            DeleteEntry,
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
            "Exit", "Monat ändern", "Eintrag löschen"
        ];

        private string[] promptFixMenu = 
        [
            "Exit", "Fixkosten-Eintrag löschen"
        ];

        private string[] promptMonthComparisonMenu = 
        [
            "Exit", 
        ];

        public Menu(int menu)
        {
            InitMenu(menu);
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
                    string monthDate = User.GetMonthInput("Monat: ");
                    month = new Month(monthDate);
                    month.Show();
                }
                else if (menuChoice == (int)EMonthMenu.DeleteEntry)
                {
                    month.Show();
                    int entryChoice = GetEntryChoice(month);
                    Entry entry = month.SelectEntry(entryChoice);
                    month.DeleteEntry(entry);
                }
            }
        }

        private static int GetEntryChoice(Month month)
        {
            if (month.EntryCount == 0)
            {
                Console.WriteLine($"Keine Einträge im ausgewähltem Monat vorhanden.");
                return -1;
            }
            string message = "Zum löschen eines Eintrags bitte Nummer eingeben: ";
            int userChoice = User.GetInputNumber(message);

            return userChoice;
        }

        private static void FixCostMenu()
        {
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
                    int fixCostChoice = GetFixCostChoice();
                    Entry entry = FixCost.SelectEntry(fixCostChoice);
                    FixCost.DeleteEntry(entry);
                }
            }
        }

        private static int GetFixCostChoice()
        {
            if (FixCost.fixCount < 1)
            {
                Console.WriteLine("Error: Es sind noch keine FixKosten-Einträge vorhanden!");
                return -1;
            }
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

        private void InitMenu(int menu)
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
        }
    }
}
