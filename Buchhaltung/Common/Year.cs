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

        public void Show()
        {
            Console.WriteLine($"\nZusammenfassung für das Jahr {date}: ");
            ShowMonthTotals();
            ShowYearTotals();
        }

        private void ShowMonthTotals()
        {
            Console.WriteLine("    Monat      |   Einkommen  |    Ausgaben    |   Übrig   ");
            foreach (Month month in loadedMonths)
            {
                Console.WriteLine($"    {month.Date}    |   {month.MoneyIncome}€    |   {month.MoneySpend}€     |   {month.MoneyLeft}€  ");
            }
        }

        private void ShowYearTotals()
        {
            Console.WriteLine($"\n    Gesamt     |   {totalIncome}€    |   {totalSpend}€    |   {totalLeft}€    ");
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