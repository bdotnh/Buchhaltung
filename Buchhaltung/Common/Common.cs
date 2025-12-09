using System.Text.Json;
using System.Text.Encodings.Web;

namespace Buchhaltung.Common
{
    public class Common
    {
        private static string currDir = Directory.GetCurrentDirectory() + """/Src/""";
        public static string FileFormat = """_data.json""";
        public static string CurrMonthFilepath = currDir + GetCurrentMonth() + FileFormat;
        public static string FixCostFilepath = currDir + """FixCosts""" + FileFormat;
        public static JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };


        public static List<string> GetFilenamesInDir(string dirPath)
        {
            List<string> allFilenames = new List<string>();
            foreach (string filename in Directory.GetFiles(dirPath))
            {
                allFilenames.Append(filename);
            }
            if (allFilenames.Count < 1)
            {
                Console.WriteLine($"""Keine Datein im Pfad: {currDir} gefunden!""");
            }

            return allFilenames;
        }

        public static List<Entry> GetEntries(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: Cant GetEntries from path({filePath})!");
                return [];
            }
            var jsonData = File.ReadAllText(filePath);
            var entryList = JsonSerializer.Deserialize<List<Entry>>(jsonData)
                             ?? new List<Entry>();

            return entryList;
        }

        public static string GetCurrentMonth()
        {
            var today = DateOnly.FromDateTime(DateTime.Now); // Todays date in mm/dd/yyyy format.
            string currentMonth = $"""{today.Month}.{today.Year}""";

            return currentMonth;
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
    }
}