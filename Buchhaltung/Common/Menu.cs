using System;
using System.Globalization;
using System.Linq.Expressions;

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
            if (menu == 2)
            {
                MonthMenu();
            }
            if (menu == 3)
            {
                FixCostMenu();
            }
        }

        private static void MonthMenu()
        {
            int menuChoice = GetMenuChoice();
            if (menuChoice == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                if (menuChoice == 1)    // Monat auswahl
                {
                    Month.ChangeDisplayedMonth();
                    Month.Show();
                } 
                else if (menuChoice == 2)   // Eintrag löschen
                {
                    Month.Show();
                    int entryChoice = GetEntryChoice();
                    Entry entry = Month.SelectEntry(entryChoice);
                    Month.DeleteEntry(entry);
                }
            }
        }
        private static void FixCostMenu()
        {
            int menuChoice = GetMenuChoice();
            if (menuChoice == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                if (menuChoice == 1) // Fixkosten-Eintrag löschen
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
            int userChoice = -1;
            string userInput = "";
            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Zum löschen eines Eintrags bitte Nummer eingeben: ");
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int temp) && temp < Month.entriesCount && temp >= 0)
                {
                    userChoice = temp;
                    isValid = true; 
                }
            }
            if (userChoice < 0)
            {
                Console.WriteLine($"Error: Keine gültige Auswahl getroffen. Auswahl: {userChoice}!");
            }

            return userChoice;
        }
    
        private static int GetFixCostChoice()
        {
            if (FixCost.fixCount < 1)
            {
                Console.WriteLine("Error: Es sind noch keine FixKosten-Einträge vorhanden!");
                return -1;
            } 
            int userChoice = -1;
            string userInput = "";
            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Zum löschen des Fixkosten-Eintrages, bitte Nummer eingeben: ");
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int temp) && temp < FixCost.fixCount && temp >= 0)
                {
                   userChoice = temp; 
                   isValid = true;
                }
            }
            if (userChoice < 0)
            {
                Console.WriteLine($"Error: Keine gültige Auswahl getroffen. Auswahl: {userChoice}!");
            }

            return userChoice; 
        } 

        private static int GetMenuChoice()
        {
            int menuChioce = -1;
            string userInput = "";
            while (string.IsNullOrEmpty(userInput))
            {
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int temp))
                {
                    menuChioce = temp;
                }
            }
            if (menuChioce < 0)
            {
                Console.WriteLine("Ups! Bei der Menü-Auswahl ist etwas schiefgelaufen. Versuch es doch diesmal mit einem existierenden Menüpunkt.");
            }

            return menuChioce;
        }

        public static void ShowPrompts(string[] menuPrompts)
        {
            Console.WriteLine("-- Menu --");
            for (int i = 0; i < menuPrompts.Length; i++)
            {
                Console.WriteLine($"{i}. {menuPrompts[i]}.");
            }
            Console.WriteLine("Eingabe: ");
        }

        private void InitMenu(int menu)
        {
            if (menu == 1)
            {
                ShowPrompts(promptEntryMenu);
            }
            else if (menu == 2)
            {
                ShowPrompts(promptMonthMenu);
            }
            else if (menu == 3)
            {
                ShowPrompts(promptFixMenu);
            }
        }
    }
}