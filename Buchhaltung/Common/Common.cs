using System;

namespace Buchhaltung.Common
{
    public class Common
    {
        public static string GetCurrentMonth()
        {
            string currentMonth = "";
            var today = DateOnly.FromDateTime(DateTime.Now); // Todays date in mm/dd/yyyy format.
            Console.WriteLine($"Current Date: {today}.");
            currentMonth = $"{today.Month}.{today.Year}";
            Console.WriteLine($"Current Month.Year: {currentMonth}.");

            return currentMonth;
        }
    }
}