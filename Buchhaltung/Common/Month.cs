using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;


namespace Buchhaltung.Common
{
    public class Month
    {
        private string[] promptMonthMenu =
        [
            "Exit",
            "Monat ändern"
        ];

        public static string monthDate = "";
        public static float entriesCount = 0.0f;
        public static float moneyEarned = 0.0f;
        public static float moneySpend = 0.0f;
        public static float moneyLeft = 0.0f;

        private static string filepath = "";
        private static List<Entry> entries = new List<Entry>();
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };

        public Month()
        {
            if (monthDate == "")
            {
                monthDate = Common.GetCurrentMonth();
            }
            LoadEntries();
            CalculateMonth();
            Show();
            entriesCount = entries.Count;
            Menu menu = new Menu(2);
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

        public static void ChangeDisplayedMonth()
        {
            string monthDate = GetMonthUserInput();
            Month.monthDate = monthDate;
        }

        public static string GetMonthUserInput()
        {
            string[] allFilesInSrcPath = Directory.GetFiles(Common.currDir);
            List<string> allMonthFilepaths = new List<string>();
            List<string> allSavedMonths = new List<string>();
            foreach (string filepath in allFilesInSrcPath)
            {
                allMonthFilepaths.Add(filepath);
                string month = filepath.Substring(filepath.LastIndexOf('/') + 1);
                month = month.Substring(0, month.LastIndexOf('_'));
                allSavedMonths.Add(month);
            }

            string userInput = "";
            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Alle gespeicherten Monate: ");
                foreach (string month in allSavedMonths)
                {
                    Console.WriteLine($"Monat: {month}.");
                }

                Console.WriteLine("Monat: ");
                userInput = Console.ReadLine();
                if (allSavedMonths.Contains(userInput))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine($"Der ausgeählte Monat: {userInput} ist leider nicht verfügbar!");
                }
            }
            monthDate = userInput;

            return userInput;
        }

        private static void InitMonth()
        {
            if (string.IsNullOrEmpty(monthDate))
            {
                monthDate = Common.GetCurrentMonth();
            } 
            filepath = Directory.GetCurrentDirectory() + "/Src/" + monthDate + "_data.json";
        }

        public static void Show()
        {
            InitMonth();
            List<Entry> entries = Common.GetEntries(filepath);
            Console.WriteLine(" ID   |     Datum     |   Betrag  |    Geschäft    | IstAusgabe | IstFix |");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($" {i}    |   {entries[i].Datum}  |   {entries[i].Betrag}    |    {entries[i].Geschäft}   |   {entries[i].IstAusgabe}     |   {entries[i].IstFix}");
            }
            Console.WriteLine($"Monatsübersicht:\nEinnahmen: {moneyEarned} €,     Ausgaben: {moneySpend} €,     Übrig: {moneyLeft} €.");
        }

        private static void CalculateMonth()
        {
            InitMonth();
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].IstAusgabe)
                {
                    moneySpend += entries[i].Betrag;
                }
                else
                {
                    moneyEarned += entries[i].Betrag;
                }
            }
            moneyLeft = moneyEarned - moneySpend;
        }

        private static void LoadEntries()
        {
            InitMonth();
            entries = Common.GetEntries(filepath);
            if (entries.Count < 1)
            {
                Console.WriteLine("Error: Es wurden keine gespeicherten Einträge gefunden!");
            }

        }
    }
}