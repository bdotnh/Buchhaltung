using System;
using System.Text.Json;

namespace Buchhaltung.Common
{
    public class Common
    {
        public static List<string> GetFilenamesInDir(string dirPath)
        {
            List<string> allFilenames = new List<string>();
            foreach (string filename in Directory.GetFiles(dirPath))
            {
                allFilenames.Append(filename);
            }
            if (allFilenames.Count < 1)
            {
                throw new Exception($"No files found in directory: {dirPath}.");
            }

            return allFilenames;
        }

        public static List<Entry> GetEntries(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            if (jsonData.Length < 1)
            {
                Console.WriteLine($"Datei: {filePath} ist leer.");
                return [];
            }
            var entryList = JsonSerializer.Deserialize<List<Entry>>(jsonData)
                             ?? new List<Entry>();

            return entryList;
        }

        public static string GetCurrentMonth()
        {
            string currentMonth = "";
            var today = DateOnly.FromDateTime(DateTime.Now); // Todays date in mm/dd/yyyy format.
            currentMonth = $"{today.Month}.{today.Year}";

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