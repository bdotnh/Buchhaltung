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
            bool isValid = false;
            while (!isValid)
            {
                string userInput = Console.ReadLine();
                if (userInput == """J""" || userInput == """j""")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public static List<int> GetNumsInput(string message)
        {
            List<int> nums = [];
            string userInput = "";
            while (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine(message);
                userInput = Console.ReadLine();
                if (userInput.Contains(","))
                {
                    string[] split = userInput.Split(",");
                    foreach (string strSplit in split)
                    {
                        if (int.TryParse(strSplit, out int num))
                        {
                            nums.Add(num);
                        }
                        else
                        {
                            Console.WriteLine($"Nummer: {strSplit} wurde nicht korrekt erfasst.");
                        }
                    }
                }
            }
            return nums;
        }

        public static int GetInputNumber(string message)
        {
            int menuChoice = -1;
            string userInput = "";
            while (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine(message);
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int temp))
                {
                    menuChoice = temp;
                }
            }
            if (menuChoice < 0)
            {
                Console.WriteLine("""Ups! Bei der Eingabe ist etwas schiefgelaufen.""");
            }

            return menuChoice;
        }

        public static string GetYearInput(string message)
        {
            string[] allFilesInSrcPath = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Src/");
            List<string> allYearFilepaths = new List<string>();
            List<string> allSavedYears = new List<string>();
            foreach (string filepath in allFilesInSrcPath)
            {
                allYearFilepaths.Add(filepath);
                string year = filepath.Substring(filepath.LastIndexOf('_') - 3, filepath.LastIndexOf('_'));
                allSavedYears.Add(year);
            }

            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("""Alle Gepseicherten Jahre: """);
                foreach (string year in allSavedYears)
                {
                    Console.WriteLine($"""Jahr: {year}.""");
                }

                Console.WriteLine(message);
                string userInput = Console.ReadLine();
                if (allSavedYears.Contains(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine($"""Zu dem ausgewählte Jahr: {userInput} wurde leider nichts gefunden!""");
                }
            }

            return "Error";
        }

        public static string GetMonthInput(string message)
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

            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("""Alle gespeicherten Monate: """);
                foreach (string month in allSavedMonths)
                {
                    Console.WriteLine($"""Monat: {month}.""");
                }

                Console.WriteLine(message);
                string userInput = Console.ReadLine();
                if (allSavedMonths.Contains(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine($"""Der ausgeählte Monat: {userInput} ist leider nicht verfügbar!""");
                }
            }

            return "Error";
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static string GetDatumInput()
        {
            // Console.Write('\r' + new string(' ', Console.WindowWidth) + '\r');
            bool isVaild = false;
            while (!isVaild)
            {
                string datumHeute = DateOnly.FromDateTime(DateTime.Now).ToString().Replace('/', '.');
                datumHeute = FormatDate(datumHeute);
                Console.WriteLine($"Datum: {datumHeute} ( Für heutiges Datum 'Enter' drücken ). Exit = 0");
                string userInput = Console.ReadLine();
                if (userInput == "")
                {
                    return datumHeute;
                }
                if (userInput == "0")
                {
                    new MainMenu();
                }
                userInput = FormatDate(userInput);
                if (userInput.Length == 10 && userInput.Count(f => f == '.') == 2)
                {
                    isVaild = true;
                    return userInput;
                }
            }

            return "Error";
        }

        public static string FormatDate(string input)
        {
            input = input.Replace(',', '.');
            if (input.IndexOf('.') == 1)
            {
                input = input.Insert(0, "0");
            }
            if (input.LastIndexOf('.') == 4)
            {
                input = input.Insert(3, "0");
            }
            if (input.Length == 10 && input.Count(f => f == '.') == 2)
            {
                return input;
            }

            return "Error";
        }

        public static float GetBetragInput()
        {
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("""Betrag: """);
                string betragInput = Console.ReadLine();
                betragInput = betragInput.Replace(',', '.');
                if (float.TryParse(betragInput, out float betragValue))
                {
                    if (betragValue > 0.0)
                    {
                        return betragValue;
                    }
                    else
                    {
                        return Math.Abs(betragValue);
                    }
                }
            }

            return -1.0f;
        }

        public static string GetGeschäftInput()
        {
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("""Geschäft: """);
                string geschäftInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(geschäftInput) && geschäftInput.Length > 2)
                {
                    return geschäftInput;
                }
            }

            return "Error";
        }

        public static bool GetIstAusgabeInput()
        {
            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("""Ausgabe? J/n: """);
                string userInput = Console.ReadLine();
                if (userInput == """J""" || userInput == """j""")
                {
                    return true;
                }
                else if (userInput == """N""" || userInput == """n""")
                {
                    return false;
                }
            }

            return true;
        }

        public static bool GetIstFixInput()
        {
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("""Zu Fixkosten hinzufügen? J/n: """);
                string userInput = Console.ReadLine();
                if (userInput == """J""" || userInput == """j""")
                {
                    return true;
                }
                else if (userInput == """N""" || userInput == """n""")
                {
                    return false;
                }
            }

            return false;
        }
    }
}