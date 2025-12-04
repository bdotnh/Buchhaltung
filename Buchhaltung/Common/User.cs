using System;

namespace Buchhaltung.Common
{
    public class User
    {
        public User()
        {

        }

        public static bool AskYesNo()
        {
            bool isYes = false;
            string userInput = "";
            bool isValid = false;
            while (!isValid)
            {
                userInput = Console.ReadLine();
                if (userInput == "J" || userInput == "j")
                {
                    isYes = true;
                    isValid = true;
                }
                else
                {
                    isValid = true;
                }
            } 

            return isYes;
        }

        public static int GetMenuChoice()
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
    }
}