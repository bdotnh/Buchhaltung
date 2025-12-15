using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Buchhaltung.Common
{
    public class Entry
    {
        public string[] Categories = ["""Datum""", """Betrag""", """Geschäft""", """EinAusgabe""", """IstFix"""];
        public string Datum { get; set; }
        public float Betrag { get; set; }
        public string Geschäft { get; set; }
        public bool IstAusgabe { get; set; }
        public bool IstFix { get; set; }

        public Entry(string datum, float betrag, string geschäft,
                        bool istAusgabe, bool istFix)
        {
            Datum = datum;
            Betrag = betrag;
            Geschäft = geschäft;
            IstAusgabe = istAusgabe;
            IstFix = istFix;
        }

        public static void Save(string filepath, Entry entry)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

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
                List<Entry> entryList = new();
                Entry lastMonthResult = GetResultLastMonth();
                if (lastMonthResult.Betrag != 0.0)
                {
                    entryList.Add(lastMonthResult);
                }
                entryList.Add(entry);
                string jsonData = JsonSerializer.Serialize(entryList, options);
                File.WriteAllText(filepath, jsonData, new UTF8Encoding());
            }
            Console.WriteLine("""Eintrag wurde gespeichert.""");

            if (entry.IstFix)
            {
                FixCost.Save(entry);
                Console.WriteLine("""Eintrag wurde zu den Fixkosten hinzugefügt.""");
            }
        }

        public static Entry GetResultLastMonth()
        {
            string dateLastMonth = Common.GetLastMonthDate();
            Entry result;  
            if (!File.Exists(Common.GetFilepathFromMonthDate(dateLastMonth)))
            {
                float savingsOrDebts = User.GetSavingsOrDebts();
                result = new Entry(dateLastMonth, savingsOrDebts, savingsOrDebts < 0 ? "Schulden" : "Erpartes", savingsOrDebts < 0, false); 
            }
            else
            {
                Month lastMonth = new(dateLastMonth);
                result = new Entry(dateLastMonth, lastMonth.MoneyLeft, "Erspartes", lastMonth.MoneyLeft < 0, false);
            }

            return result;
        }

        public static Dictionary<string, object> GetInputs()
        {
            Dictionary<string, object> Inputs = new Dictionary<string, object>
            {
            };
            string datum = User.GetDatumInput();
            if (!Inputs.TryAdd("""Datum""", datum))
            {
                Console.WriteLine($"""Fehler beim speichern des Datums: {datum}!""");
            }
            float betrag = User.GetBetragInput();
            string geschäft = User.GetGeschäftInput();
            if (!Inputs.TryAdd("""Geschäft""", geschäft))
            {
                Console.WriteLine($"""Fehler beim speichern des Geschäfts: {geschäft}!""");
            }
            bool istAusgabe = User.GetIstAusgabeInput();
            if (!Inputs.TryAdd("""IstAusgabe""", istAusgabe))
            {
                Console.WriteLine($"""Fehler beim speichern ist Ausgabe: {istAusgabe}!""");
            }
            if (istAusgabe == true)
            {
                betrag = -betrag;
            }
            if (!Inputs.TryAdd("""Betrag""", betrag))
            {
                Console.WriteLine($"""Fehler beim speichern des Betrags: {betrag}!""");
            }
            bool istFix = User.GetIstFixInput();
            if (!Inputs.TryAdd("""IstFix""", istFix))
            {
                Console.WriteLine($"""Fehler beim speichern ist Fix: {istFix}!""");
            }

            return Inputs;
        }
    }
}