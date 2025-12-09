using System;

namespace Buchhaltung.Common
{
    public class MonthComparison
    {
        private static Month Month1;
        private static Month Month2;
        private static Dictionary<string, float> Diffs { get; set; }

        public MonthComparison(string month1, string month2)
        {
            Month1 = new Month(month1);
            Month2 = new Month(month2);
            CalculateDiffs();
        }

        public void ShowDiffs()
        {
            ShowTotalDiffs();
        }

        private static void ShowTotalDiffs()
        {
            foreach (var diff in Diffs)
            {
                if (!Diffs.TryGetValue(diff.Key, out float diffValue))
                {
                    Console.WriteLine($"Error: Cant get {diff.Key}s Value!");
                }
            }

            Console.WriteLine($"Einkommen:\nMonat {Month1.Date}: {Math.Round(Month1.MoneyIncome, 2)} €. Monat {Month2.Date}: {Math.Round(Month2.MoneyIncome, 2)} €");
            Console.WriteLine($"Differenz: {Diffs["Income"]} € ({Diffs["IncomeProz"]} %)");

            Console.WriteLine($"Ausgaben:\nMonat {Month1.Date}: {Math.Round(Month1.MoneySpend, 2)} €. Monat {Month2.Date}: {Math.Round(Month2.MoneySpend, 2)} €");
            Console.WriteLine($"Differenz: {Diffs["Spend"]} € ({Diffs["SpendProz"]} %)");

            Console.WriteLine($"Erpartes:\nMonat {Month1.Date}: {Math.Round(Month1.MoneyLeft, 2)} €. Monat {Month2.Date}: {Math.Round(Month2.MoneyLeft, 2)} €");
            Console.WriteLine($"Differenz: {Diffs["Left"]} € ({Diffs["LeftProz"]} %)\n");
        }

        private static void CalculateDiffs()
        {
            float incomeDiff = GetDifference(Month1.MoneyIncome, Month2.MoneyIncome);
            float spendDiff = GetDifference(Month1.MoneySpend, Month2.MoneySpend);
            float leftDiff = GetDifference(Month1.MoneyLeft, Month2.MoneyLeft);
            float incomeDiffProz = GetPercentageChange(Month1.MoneyIncome, Month2.MoneyIncome);
            float spendDiffProz = GetPercentageChange(Month1.MoneySpend, Month2.MoneySpend);
            float leftDiffProz = GetPercentageChange(Month1.MoneyLeft, Month2.MoneyLeft);
            Diffs = new()
            {
                { "Income", incomeDiff }, { "Spend", spendDiff }, { "Left", leftDiff }, { "IncomeProz", incomeDiffProz }, { "SpendProz", spendDiffProz }, { "LeftProz", leftDiffProz }
            };
        }

        public static float GetDifference(float amount1, float amount2)
        {
            double value = 0.0f;
            if (amount1 != amount2 && amount1 < amount2)
            {
                value = amount2 - amount1;
            }
            else if (amount1 != amount2 && amount1 > amount2)
            {
                value = amount1 - amount2;
            }
            value = Math.Round(value, 2);
            float res = (float)value;

            return res;
        }

        public static float GetPercentageChange(float startValue, float endValue)
        {
            double diff = 100 * (endValue - startValue) / startValue;
            double diffRounded = Math.Round(diff, 2);
            float res = (float)diffRounded;

            return res;
        }
    }
}