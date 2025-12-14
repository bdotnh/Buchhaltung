using System;

namespace Buchhaltung.Common
{
    public class MonthComparison : Comparison
    {
        private static Month month1;
        private static Month month2;
        private static Dictionary<string, float> Diffs { get; set; }

        public MonthComparison(string date1, string date2)
        {
            if (date1 == date2)
            {
                Console.WriteLine($"Error: Es wurde zweimal der selbe Monat eingeben (1: {date1}, 2: {date2})!");
            }
            else
            {
                month1 = new Month(date1);
                month2 = new Month(date2);
                CalculateDiffs();
            }
        }

        public override void ShowComparison()
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

            Console.WriteLine($"\nEinkommen:\nMonat {month1.Date}: {Math.Round(month1.MoneyIncome, 2)}€, Monat {month2.Date}: {Math.Round(month2.MoneyIncome, 2)}€, Differenz: {Diffs["Income"]}€ ({Diffs["IncomeProz"]}%)");

            Console.WriteLine($"Ausgaben:\nMonat {month1.Date}: {Math.Round(month1.MoneySpend, 2)}€, Monat {month2.Date}: {Math.Round(month2.MoneySpend, 2)}€, Differenz: {Diffs["Spend"]}€ ({Diffs["SpendProz"]}%)");

            Console.WriteLine($"Erpartes:\nMonat {month1.Date}: {Math.Round(month1.MoneyLeft, 2)}€, Monat {month2.Date}: {Math.Round(month2.MoneyLeft, 2)}€, Differenz: {Diffs["Left"]}€ ({Diffs["LeftProz"]}%)");
        }

        protected override void CalculateDiffs()
        {
            float incomeDiff = GetDifference(month1.MoneyIncome, month2.MoneyIncome);
            float spendDiff = GetDifference(month1.MoneySpend, month2.MoneySpend);
            float leftDiff = GetDifference(month1.MoneyLeft, month2.MoneyLeft);
            float incomeDiffProz = GetPercentageChange(month1.MoneyIncome, month2.MoneyIncome);
            float spendDiffProz = GetPercentageChange(month1.MoneySpend, month2.MoneySpend);
            float leftDiffProz = GetPercentageChange(month1.MoneyLeft, month2.MoneyLeft);
            Diffs = new()
            {
                { "Income", incomeDiff }, { "Spend", spendDiff }, { "Left", leftDiff }, { "IncomeProz", incomeDiffProz }, { "SpendProz", spendDiffProz }, { "LeftProz", leftDiffProz }
            };
        }
    }
}