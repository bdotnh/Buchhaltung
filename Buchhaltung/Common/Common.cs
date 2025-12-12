using System.Text.Json;
using System.Text.Encodings.Web;

namespace Buchhaltung.Common
{
    public class Common
    {
        public static string CurrDir = Directory.GetCurrentDirectory() + """/Src/""";
        public static string FileFormat = """_data.json""";
        public static string CurrMonthFilepath = CurrDir + GetCurrentMonth() + FileFormat;
        public static string FixCostFilepath = CurrDir + """FixCosts""" + FileFormat;
        public static JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };


        public static List<string> GetFilenamesInDir()
        {
            List<string> allFilenames = new List<string>();
            foreach (string filename in Directory.GetFiles(CurrDir))
            {
                allFilenames.Append(filename);
            }
            if (allFilenames.Count < 1)
            {
                Console.WriteLine($"""Keine Datein im Pfad: {CurrDir} gefunden!""");
            }

            return allFilenames;
        }

        public static List<Entry> GetEntries(string filepath)
        {
            List<Entry> entries = new();
            string jsonData = "";
            if (File.Exists(filepath))
            {
                try
                {
                    jsonData = File.ReadAllText(filepath);
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        entries = JsonSerializer.Deserialize<List<Entry>>(jsonData);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

            return entries;
        }

        public static int GetUserInput(int maxCondition, int minCondition = 0)
        {
            int res = 0;
            string? userInput = Console.ReadLine();
            bool isValid = false;
            while (!isValid)
            {
                if (int.TryParse(userInput, out int temp))
                {
                    if (temp < maxCondition && temp > minCondition)
                    {
                        res = temp;
                        isValid = true;
                    }
                }
            }

            return res;
        }

        public static string GetCurrentMonth()
        {
            var today = DateOnly.FromDateTime(DateTime.Now); // Todays date in mm/dd/yyyy format.
            string currentMonth = $"""{today.Month}.{today.Year}""";

            return currentMonth;
        }

        public static string GetCurrentYear()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            string currentYear = $""""{today.Year}"""";
        
            return currentYear;
        }
    }
}