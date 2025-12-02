using System.Globalization;

namespace Buchhaltung.Common
{
    public abstract class Menu
    {
        public string _menuName { get; set; }
        public string[] _menuChoices { get; set; }

        protected Menu(string menuName, string[] menuChoices)
        {
            this._menuName = menuName;
            this._menuChoices = menuChoices;
        }

        protected void ShowMenuChoices(string menuName)
        {
            Console.WriteLine($"-- {menuName}-Menü --");
            for (int i = 0; i < _menuChoices.Length; i++)
            {
                Console.WriteLine($"{i}. {_menuChoices[i]}.");
            }
            Console.Write("Menüpunkt auswählen: ");
        }

        protected int GetMenuChoice(string menuName)
        {
            int userChoice = -1;
            while (userChoice == -1)
            {
                ShowMenuChoices(menuName);
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int userChoiceResult))
                {
                    userChoice = userChoiceResult;
                    break;
                }
            }
            
            return userChoice;
        }
    }

    public class MainMenu
    {
        private static string[] menu =
        [
            "0. Exit",
            "1. Neuer Eintrag",
            "2. Monats-Übersicht",   
            "3. Fix Kosten", 
        ];
        private static void ShowMenu()
        {
            Console.WriteLine("-- Menü --");
            foreach (string menuIndex in menu)
            {
                Console.WriteLine(menuIndex);
            }
        }
        public bool IsMenuChoiceValid(string userInput)
        {
            int temp = 0;
            if (userInput == " ")
            {
                Console.WriteLine("Eingabe darf nicht leer sein!");
                return false;
            }
            if (!int.TryParse(userInput, out temp))
            {
                Console.WriteLine("Eingabe muss nummer sein!");
                return false;
            }

            if (temp < 0 || temp > menu.Length)
            {
                Console.WriteLine($"Nummer darf nicht kleiner 0 oder größer als {menu.Length} sein!");
                return false;
            }
            
            return true;
        }

        public MainMenu()
        {
            string userInput = "";
            ShowMenu();    
            while (userInput == "" && !IsMenuChoiceValid(userInput))
            {
                userInput = Console.ReadLine();
                if (userInput == "0")
                {
                    Environment.Exit(0);
                }
            }
            int menuChoice = Convert.ToInt32(userInput);
            switch (menuChoice)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Entry.Save();
                    break;
                case 2:
                    _ = new DisplayMonth();
                    break;
            }
        }
    }
}