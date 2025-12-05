using System;

namespace Buchhaltung.Common
{
    public class Menu
    {
        public static string[] menuPrompts = [];

        private string[] promptEntryMenu =
        [
            "Exit",
        ];

        private string[] promptMonthMenu =
        [
            "Exit",
            "Monat ändern",
            "Eintrag löschen"
        ];

        private string[] promptFixMenu =
        [
            "Exit",
            "Fixkosten-Eintrag löschen"
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

        public enum EMonthMenu
        {
            Exit = 0, ChangeMonth, DeleteEntry
        }

        private static void MonthMenu()
        {
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
                    Month.ChangeDisplayedMonth();
                    Month.Show();
                }
                else if (menuChoice == (int)EMonthMenu.DeleteEntry)   
                {
                    Month.Show();
                    int entryChoice = GetEntryChoice();
                    Entry entry = Month.SelectEntry(entryChoice);
                    Month.DeleteEntry(entry);
                }
            }
        }

        public enum EFixMenu
        {
            Exit = 0, DeleteEntry
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

        private static int GetEntryChoice()
        {
            if (Month.entriesCount == 0)
            {
                Console.WriteLine($"Keine Einträge im Monat: {Month.monthDate} vorhanden.");
                return -1;
            }
            string message = "Zum löschen eines Eintrags bitte Nummer eingeben: ";
            int userChoice = User.GetInputNumber(message);

            return userChoice;
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