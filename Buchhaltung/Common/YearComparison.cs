using System;

namespace Buchhaltung.Common
{
    public class YearComparison : Comparison
    {
        private static Year year1;
        private static Year year2;
        private static Dictionary<string, float> Diffs { get; set; } 
    
        public YearComparison(string date1, string date2)
        {
            if (date1 == date2)
            {
                Console.WriteLine($"Error: Es wurde zweimal das selbe Jahr eingeben ({date1})!");
            }
            else
            {
                year1 = new(date1);
                year2 = new(date2);
                CalculateDiffs();
            }
        }

        public override void ShowTotalDiffs()
        {
            try
            {
                foreach (var diff in Diffs)
                {
                    if (!Diffs.TryGetValue(diff.Key, out float diffValue))
                    {
                        Console.WriteLine($"Error: Cant get {diff.Key}s Value!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.WriteLine($"Einkommen:\nMonat {year1.Date}: {Math.Round(year1.TotalIncome, 2)}€, Monat {year2.Date}: {Math.Round(year2.TotalIncome, 2)}€, Differenz: {Diffs["Income"]}€ ({Diffs["IncomeProz"]}%)");

            Console.WriteLine($"Ausgaben:\nMonat {year1.Date}: {Math.Round(year1.TotalSpend, 2)}€, Monat {year2.Date}: {Math.Round(year2.TotalSpend, 2)}€, Differenz: {Diffs["Spend"]}€ ({Diffs["SpendProz"]}%)");

            Console.WriteLine($"Erpartes:\nMonat {year1.Date}: {Math.Round(year1.TotalLeft, 2)}€, Monat {year2.Date}: {Math.Round(year2.TotalLeft, 2)}€, Differenz: {Diffs["Left"]}€ ({Diffs["LeftProz"]}%)\n");
        }

        protected override void CalculateDiffs()
        {
            float incomeDiff = GetDifference(year1.TotalIncome, year2.TotalIncome);
            float spendDiff = GetDifference(year1.TotalSpend, year2.TotalSpend);
           
            float leftDiff = GetDifference(year1.TotalLeft, year2.TotalLeft);
            float incomeDiffProz = GetPercentageChange(year1.TotalIncome, year2.TotalIncome);
            float spendDiffProz = GetPercentageChange(year1.TotalSpend, year2.TotalSpend);
            float leftDiffProz = GetPercentageChange(year1.TotalLeft, year2.TotalLeft);
            Diffs = new()
            {
                { "Income", incomeDiff }, { "Spend", spendDiff }, { "Left", leftDiff }, { "IncomeProz", incomeDiffProz }, { "SpendProz", spendDiffProz }, { "LeftProz", leftDiffProz }
            };
        }
    }
}