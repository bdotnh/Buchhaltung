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
                if (userInput == """J""" || userInput == """j""")
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

        public static int GetInputNumber(string message)
        {
            int menuChioce = -1;
            string userInput = "";
            while (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine(message);
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int temp))
                {
                    menuChioce = temp;
                }
            }
            if (menuChioce < 0)
            {
                Console.WriteLine("""Ups! Bei der Eingabe ist etwas schiefgelaufen.""");
            }

            return menuChioce;
        }

        public static string GetMonthInput()
        {
            string[] allFilesInSrcPath = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Src/");
            List<string> allMonthFilepaths = new List<string>();
            List<string> allSavedMonths = new List<string>();
            foreach (string filepath in allFilesInSrcPath)
            {
                allMonthFilepaths.Add(filepath);
                string month = filepath.Substring(filepath.LastIndexOf('/') + 1);
                month = month.Substring(0, month.LastIndexOf('_'));
                allSavedMonths.Add(month);
            }

            string userInput = "";
            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("""Alle gespeicherten Monate: """);
                foreach (string month in allSavedMonths)
                {
                    Console.WriteLine($"""Monat: {month}.""");
                }

                Console.WriteLine("""Monat: """);
                userInput = Console.ReadLine();
                if (allSavedMonths.Contains(userInput))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine($"""Der ausgeählte Monat: {userInput} ist leider nicht verfügbar!""");
                }
            }
            Month.monthDate = userInput;

            return userInput;
        }

        public static string GetDatumInput()
        {
            string userInput = "";
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("""Datum: """);
                userInput = Console.ReadLine();
                if (!String.IsNullOrEmpty(userInput))
                {
                    _ = userInput.Replace(""",""", """.""");
                    if (userInput.IndexOf('.') == 1)
                    {
                        _ = userInput.Insert(0, """0""");
                    }

                    if (userInput.LastIndexOf('.') == 4)
                    {
                        _ = userInput.Insert(3, """0""");
                    }

                    if (userInput.Length == 10 && userInput.Count(f => f == '.') == 2)
                    {
                        isVaild = true;
                    }
                }
            }

            return userInput; // 00.00.0000
        }

        public static float GetBetragInput()
        {
            string? betragInput;
            float betragValue = -1.0f;
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("""Betrag: """);
                betragInput = Console.ReadLine();
                _ = betragInput.Replace(',', '.');
                if (float.TryParse(betragInput, out betragValue))
                {
                    if (betragValue > 0.0f)
                    {
                        isVaild = true;
                    }
                }
            }

            return betragValue;
        }

        public static string GetGeschäftInput()
        {
            string GeschäftInput = "";
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("""Geschäft: """);
                GeschäftInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(GeschäftInput) && GeschäftInput.Length > 2)
                {
                    isVaild = true;
                    break;
                }
            }

            return GeschäftInput;
        }

        public static bool GetIstAusgabeInput()
        {
            bool istAusgabe = false;
            bool isValid = false;
            string userInput = "";
            while (!isValid)
            {
                Console.WriteLine("""Ausgabe? J/n: """);
                userInput = Console.ReadLine();
                if (userInput == """J""" || userInput == """j""")
                {
                    istAusgabe = true;
                    isValid = true;
                    break;
                }
                else if (userInput == """N""" || userInput == """n""")
                {
                    istAusgabe = false;
                    isValid = true;
                    break;
                }
            }

            return istAusgabe;
        }

        public static bool GetIstFixInput()
        {
            bool istFix = false;
            bool isValid = false;
            string userInput = "";
            while (!isValid)
            {
                Console.Write("""Zu Fixkosten hinzufügen? J/n: """);
                userInput = Console.ReadLine();
                if (userInput == """J""" || userInput == """j""")
                {
                    istFix = true;
                    isValid = true;
                }
                else if (userInput == """N""" || userInput == """n""")
                {
                    istFix = false;
                    isValid = true;
                }
            }

            return istFix;
        }

    }
}