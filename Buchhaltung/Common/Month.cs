using System;
using System.Text;
using System.Text.Encodings.Web;
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
            date = Date;
            LoadEntries();
            CalculateMonth();
        }

        public void DeleteEntry(Entry entry)
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

        public Entry SelectEntry(int number)
        {
            Entry selectedEntry = entries[number];

            return selectedEntry;
        }

        public int SaveAllEntries()
        {
            if (!File.Exists(filepath))
            {
                return -1;
            }
            var jsonData = JsonSerializer.Serialize(entries, Common.JsonOptions);
            File.WriteAllText(filepath, jsonData, new UTF8Encoding());

            return 0;
        }

        public void Show()
        {
            List<Entry> entries = Common.GetEntries(filepath);
            Console.WriteLine(" ID   |     Datum     |   Betrag  |    Geschäft    | IstAusgabe | IstFix |");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($" {i}    |   {entries[i].Datum}  |   {entries[i].Betrag}    |    {entries[i].Geschäft}   |   {entries[i].IstAusgabe}     |   {entries[i].IstFix}");
            }
            ShowTotals();
        }

        private void ShowTotals()
        {
            Console.WriteLine($"Monatsübersicht:\nEinkommen: {moneyIncome} €,     Ausgaben: {moneySpend} €,     Gespart: {moneyLeft} €.");
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
            filepath = Directory.GetCurrentDirectory() + "/Src/" + date + "_data.json";
            entries = Common.GetEntries(filepath);
            if (entries.Count < 1)
            {
                Console.WriteLine("Error: Es wurden keine gespeicherten Einträge gefunden!");
            }

        }
    }
}