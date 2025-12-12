using System;
using System.Data;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;



namespace Buchhaltung.Common
{
    public class FixCost
    {
        public static int fixCount = 0;
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        private static string filepath = Common.FixCostFilepath;
        private static List<Entry> entries = new List<Entry>();
        private static float moneyIncome = 0.0f;
        private static float moneySpend = 0.0f;
        private static float moneyLeft = 0.0f;

        public FixCost()
        {
            LoadEntries();
            CalculateTotals();
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
            List<Entry> selectedEntries = new();
            for (int i = 0; i < nums.Count; i++)
            {
                selectedEntries.Add(entries[i]);
            }
            return selectedEntries;
        }

        public static int SaveEntries()
        {
            if (!File.Exists(filepath))
            {
                return -1;
            }
            var jsonData = JsonSerializer.Serialize(entries, options);
            File.WriteAllText(filepath, jsonData, new UTF8Encoding());

            return 0;
        }

        public static void Save(Entry entry)
        {
            if (File.Exists(filepath) && File.ReadAllLines(filepath).Length > 1)
            {
                var jsonData = File.ReadAllText(filepath);
                var entryList = JsonSerializer.Deserialize<List<Entry>>(jsonData)
                                ?? new List<Entry>();
                entryList.Add(entry);
                jsonData = JsonSerializer.Serialize(entryList, options);
                File.WriteAllText(filepath, jsonData, new UTF8Encoding());
            }
            else
            {
                var entryList = new List<Entry>() { entry };
                string jsonData = JsonSerializer.Serialize(entryList, options);
                File.WriteAllText(filepath, jsonData, new UTF8Encoding());
            }
        }

        public void Show()
        {
            fixCount = entries.Count;
            Console.WriteLine(" ID   |     Datum     |   Betrag  |    Geschäft    | Ein-/Ausgabe | IstFix |");
            for (int i = 0; i < fixCount; i++)
            {
                Console.WriteLine($" {i}    |   {entries[i].Datum}  |   {entries[i].Betrag}€    |    {entries[i].Geschäft}   |   {Month.FormatWasSpended(entries[i].IstAusgabe)}     |   {Month.FormatIsFix(entries[i].IstFix)}");
            }
            ShowTotals();
        }

        private void ShowTotals()
        {
            Console.WriteLine($"Einkommen: {moneyIncome}€,     Ausgaben: {moneySpend}€,     Übrig: {moneyLeft}€.");
        }

        private void CalculateTotals()
        {
            moneySpend = 0.0f;
            moneyLeft = 0.0f;
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

        private static void LoadEntries()
        {
            entries = Common.GetEntries(filepath);
            if (entries.Count < 1)
            {
                Console.WriteLine("Error: Es wurden keine gespeicherten Fixkosten gefunden!");
            }
        }
    }
}