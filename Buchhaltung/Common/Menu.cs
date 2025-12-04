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
            "Monat ändern"
        ];

        private string[] promptFixMenu =
        [
            "Exit",
            "Fix Kosten anpassen"
        ];

        public Menu(int menu)
        {
            InitMenu(menu);
            if (menu == 2)
            {
                MonthMenu();
            }

        }

        private static void FixMenu()
        {
            int menuChoice = GetMenuChoice();
            if (menuChoice == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                if (menuChoice == 1)
                {
                    GetFixCostChoice();
                }
            }
        }

        private static int GetFixCostChoice()
        {
            int userChoice = -1;

            if (userChoice < 0)
            {
                Console.WriteLine($"Error: Keine gültige Auswahl getroffen. Auswahl: {userChoice}!");
            }

            return userChoice; 
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
                if (menuChoice == 1)
                {
                    ChangeDisplayedMonth();
                }
            }

        }

        private static void ChangeDisplayedMonth()
        {
            string monthDate = Month.GetMonthUserInput();
            Month.Show(monthDate);
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