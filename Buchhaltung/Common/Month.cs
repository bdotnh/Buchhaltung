using System;
using System.Text;
using System.Text.Json;


namespace Buchhaltung.Common
{
    public class Month
    {
        private string date { get; set; }
        public string Date { get => date; }
        private float moneyIncome { get; set; }
        public float MoneyIncome { get => moneyIncome; }
        private float moneySpend { get; set; }
        public float MoneySpend { get => moneySpend; }
        private float moneyLeft { get; set; }
        public float MoneyLeft { get => moneyLeft; }
        private List<Entry> entries = new List<Entry>();
        public int EntryCount { get => entries.Count; }
        private string filepath = "";

        public Month(string dateInput)
        {
            if (string.IsNullOrEmpty(dateInput))
            {
                date = Common.GetCurrentMonth();
            }
            else
            {
                date = dateInput;
            }
            filepath = Directory.GetCurrentDirectory() + "/Src/" + date + "_data.json";
            if (File.Exists(filepath))
            {
                LoadEntries();
                CalculateMonth();
            }
            else
            {
                Console.WriteLine($"Monat: {date} ist leer.");
            }
        }

        public void DeleteEntries(List<Entry> list)
        {
            if (list.Count == 0 && entries.Count == 1)
            {
                entries = new List<Entry>();
            }
            else
            {

                for (int i = 0; i < list.Count; i++)
                {
                    entries.Remove(list[i]);
                }
            }
            SaveEntries();
        }

        public List<Entry> SelectEntries(List<int> nums)
        {
            nums.Sort();
            nums.Reverse();
            List<Entry> list = new();
            for (int i = 0; i < nums.Count; i++)
            {
                list.Add(entries[nums[i]]);
            }

            return list;
        }

        public void SaveEntries()
        {
            var jsonData = JsonSerializer.Serialize(entries, Common.JsonOptions);
            File.WriteAllText(filepath, jsonData, new UTF8Encoding());
        }

        public void Show()
        {
            Console.WriteLine("\n ID   |     Datum     |   Betrag     |    Geschäft    |   Ein-/Ausgabe   |   IstFix");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($" {i}    |   {entries[i].Datum}  |   {entries[i].Betrag}€      |      {entries[i].Geschäft}      |    {FormatWasSpended(entries[i].IstAusgabe)}      |   {FormatIsFix(entries[i].IstFix)}");
            }
            ShowMonthTotals();
        }

        private void ShowMonthTotals()
        {
            Console.WriteLine($"Einkommen: {moneyIncome}€, Ausgaben: {moneySpend}€, Ersparnis: {moneyLeft}€.");
        }

        public static string FormatWasSpended(bool input)
        {
            string res;
            if (input == true)
            {
                res = "Ausgabe";
            }
            else
            {
                res = "Einnahme";
            }

            return res;
        }

        public static string FormatIsFix(bool input)
        {
            string res;
            if (input == true)
            {
                res = "Ja";
            }
            else
            {
                res = "-";
            }

            return res;
        }

        private void CalculateMonth()
        {
            moneySpend = 0.0f;
            moneyIncome = 0.0f;
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].IstAusgabe)
                {
                    moneySpend += entries[i].Betrag;
                }
                else
                {
                    moneyIncome += entries[i].Betrag;
                }
            }
            float diff = moneyIncome + moneySpend;
            moneyLeft = (float)Math.Round(diff, 2); 
        }

        private void LoadEntries()
        {
            if (File.Exists(filepath))
            {
                try
                {
                    entries = Common.GetEntries(filepath);
                    if (entries.Count < 1)
                    {
                        Console.WriteLine("Error: Es wurden keine gespeicherten Einträge gefunden!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }

        }
    }
}