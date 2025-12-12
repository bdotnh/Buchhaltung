using System;

namespace Buchhaltung.Common
{
    public class Year
    {
        private string date { get; set; }
        public string Date { get => date; }
        private string[] allFilenames { get; set; }
        private List<string> monthDates = new List<string>();
        private List<Month> loadedMonths = new List<Month>();
        private float totalIncome { get; set; }
        public float TotalIncome { get => totalIncome; }
        private float totalSpend { get; set; }
        public float TotalSpend { get => totalSpend; }
        private float totalLeft { get; set; }
        public float TotalLeft { get => totalLeft; }
        public Year(string dateInput)
        {
            if (string.IsNullOrEmpty(dateInput))
            {
                date = Common.GetCurrentYear();
            }
            else
            {
                date = dateInput;
            }
            try
            {

                LoadMonthFilenames();
                LoadMonths();
                CalculateYearTotals();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        public void ShowYearTotals()
        {
            Console.WriteLine($"Zusammenfassung für das Jahr {date}:\nEinkommen: {totalIncome}€, Ausgaben: {totalSpend}€, Übrig: {totalLeft}€.");
        }

        private void CalculateYearTotals()
        {
            totalIncome = 0.0f; totalSpend = 0.0f; totalLeft = 0.0f;
            foreach (Month month in loadedMonths)
            {
                totalIncome += month.MoneyIncome;
                totalSpend += month.MoneySpend;
                totalLeft += month.MoneyLeft;
            }
        }

        public void ShowAllMonthTotals()
        {
            foreach (Month month in loadedMonths)
            {
                Console.WriteLine($"Monat {month.Date}: Einkommen: {month.MoneyIncome}€, Ausgaben: {month.MoneySpend}€, Übrig: {month.MoneyLeft}€.");
            }
        }

        private void LoadMonths()
        {
            foreach (string monthDate in monthDates)
            {
                Month month = new Month(monthDate);
                loadedMonths.Add(month);
            }
        }

        private void LoadMonthFilenames()
        {
            allFilenames = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Src/");
            try
            {
                foreach (string filename in allFilenames)
                {
                    if (filename.Contains(date))
                    {
                        string monthDate = filename.Substring(filename.LastIndexOf('/') + 1, 7);
                        monthDates.Add(monthDate);
                    }
                }
                if (monthDates.Count == 0)
                {
                    Console.WriteLine($"Error: Es wurden keine Dateien für das Jahr {date} gefunden!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}