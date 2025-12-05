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
        private static float fixMoneySpend = 0.0f;
        private static float fixMoneyEarned = 0.0f;
        private static float moneyLeft = 0.0f;
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        private static string filepath = Common.fixCostFilepath;
        private static List<Entry> entries = new List<Entry>();

        public FixCost()
        {
            LoadEntries();
            Show();
            Menu menu = new Menu(3);
        }

        public static void DeleteEntry(Entry entry)
        {
            entries.Remove(entry);
            if (SaveAllEntries() == 0)
            {
                Console.WriteLine("Fixkosten-Eintrag wurde erfolgreich gelöscht.");
            }
            else
            {
                Console.WriteLine("Error: Fehler beim löschen des Fixkosten-Eintrags!");
            }
        }

        public static Entry SelectEntry(int number)
        {
            Console.WriteLine($"Ausgewählte Nummer: {number}.");
            Entry selectedEntry = entries[number];

            return selectedEntry;
        }

        public static int SaveAllEntries()
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
                var entryList = new List<Entry>
                {
                    entry
                };
                string jsonData = JsonSerializer.Serialize(entryList, options);
                File.WriteAllText(filepath, jsonData, new UTF8Encoding());
            }
        }

        public static void Show()
        {
            fixCount = entries.Count;
            Console.WriteLine(" ID   |     Datum     |   Betrag  |    Geschäft    | IstAusgabe | IstFix |");
            for (int i = 0; i < fixCount; i++)
            {
                Console.WriteLine($" {i}    |   {entries[i].Datum}  |   {entries[i].Betrag}    |    {entries[i].Geschäft}   |   {entries[i].IstAusgabe}     |   {entries[i].IstFix}");
            }
            if (moneyLeft > 0.0f)
            {
                Console.WriteLine($"Überblick der Fixkosten:\nGesamt-Einkommen: {fixMoneyEarned} €,      Ausgaben: {fixMoneySpend} €,   Einkommen abzüglich Fixkosten: {moneyLeft} € .");
            }
            else
            {
                Console.WriteLine($"Feste monatliche Ausgaben: {fixMoneySpend} €.");
            }
        }

        private static void CalculateFixCosts()
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].IstAusgabe)
                {
                    fixMoneySpend += entries[i].Betrag;
                }
                else
                {
                    fixMoneyEarned += entries[i].Betrag;
                }
                if (fixMoneyEarned > 0.0f)
                {
                    moneyLeft = fixMoneyEarned - fixMoneySpend;
                }
            }
        }

        private static void LoadEntries()
        {
            entries = Common.GetEntries(filepath);
            if (entries.Count < 1)
            {
                Console.WriteLine("Error: Es wurden keine gespeicherten Fixkosten gefunden!");
            }
            CalculateFixCosts();
        }
    }
}