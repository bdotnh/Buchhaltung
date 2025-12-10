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

        public Month(string Date)
        {
            if (string.IsNullOrEmpty(date))
            {
                date = Common.GetCurrentMonth();
            }
            filepath = Directory.GetCurrentDirectory() + "/Src/" + date + "_data.json";
            if (File.Exists(filepath))
            {
                LoadEntries();
                CalculateMonth();
            }
            else
            {
                Console.WriteLine($"Monat: {Date} ist leer.");
            }
        }

        public void DeleteEntry(Entry entry)
        {
            entries.Remove(entry);
            SaveEntries(); 
        }

        public Entry SelectEntry(int number)
        {
            Entry selectedEntry = entries[number];

            return selectedEntry;
        }

        public void SaveEntries()
        {
            var jsonData = JsonSerializer.Serialize(entries, Common.JsonOptions);
            File.WriteAllText(filepath, jsonData, new UTF8Encoding());
        }

        public void Show()
        {
            Console.WriteLine(" ID   |     Datum     |   Betrag     |    Geschäft    |   IstAusgabe   |   IstFix");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($" {i}    |   {entries[i].Datum}  |   {entries[i].Betrag}      |      {entries[i].Geschäft}      |    {FormatWasSpended(entries[i].IstAusgabe)}      |   {FormatIsFix(entries[i].IstFix)}");
            }
            ShowTotals();
        }

        private void ShowTotals()
        {
            Console.WriteLine($"Monatsübersicht:\nEinkommen: {moneyIncome} €,     Ausgaben: {moneySpend} €,     Gespart: {moneyLeft} €.");
        }

        private string FormatWasSpended(bool input)
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

        private string FormatIsFix(bool input)
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
            moneyLeft = moneyIncome - moneySpend;

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