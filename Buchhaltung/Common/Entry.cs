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
        private static List<Entry> entries = new List<Entry>();

        public Entry(string datum, float betrag, string geschäft,
                        bool istAusgabe, bool istFix)
        {
            this.Datum = datum;
            this.Betrag = betrag;
            this.Geschäft = geschäft;
            this.IstAusgabe = istAusgabe;
            this.IstFix = istFix;
        }

        public static void Save(string filePath, Entry entry)
        {

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            if (File.Exists(filePath) && File.ReadAllLines(filePath).Length > 1)
            {
                var jsonData = File.ReadAllText(filePath);
                var entryList = JsonSerializer.Deserialize<List<Entry>>(jsonData)
                                ?? new List<Entry>();
                entryList.Add(entry);
                jsonData = JsonSerializer.Serialize(entryList, options);
                File.WriteAllText(filePath, jsonData, new UTF8Encoding());
            }
            else
            {
                var entryList = new List<Entry>();
                entryList.Add(entry);
                string jsonData = JsonSerializer.Serialize(entryList, options);
                File.WriteAllText(filePath, jsonData, new UTF8Encoding());
            }
            Console.WriteLine("""Eintrag wurde gespeichert.""");
            
            if (entry.IstFix)
            {
                FixCost.Save(entry);
                Console.WriteLine("""Eintrag wurde zu den Fixkosten hinzugefügt."""); 
            }
        }

        public static Dictionary<string, object> GetInputs()
        {
            Dictionary<string, object> Inputs = new Dictionary<string, object>
            {
                { """Datum""", "" },
                { """Betrag""", 0.0f },
                { """Geschäft""", "" },
                { """IstAusgabe""", true },
                { """IstFix""", false }
            };

            string datum = User.GetDatumInput();
            if (!Inputs.TryAdd("""Datum""", datum))
            {
                Console.WriteLine($"""Fehler beim speichern des Datums: {datum}!""");
            }
            float betrag = User.GetBetragInput();
            if (!Inputs.TryAdd("""Betrag""", betrag))
            {
                Console.WriteLine($"""Fehler beim speichern des Betrags: {betrag}!""");
            }
            string geschäft = User.GetGeschäftInput();
            if (!Inputs.TryAdd("""Geschäft""", geschäft))
            {
                Console.WriteLine($"""Fehler beim speichern des Datums: {geschäft}!""");
            }
            bool istAusgabe = User.GetIstAusgabeInput();
            if (!Inputs.TryAdd("""IstAusgabe""", istAusgabe))
            {
                Console.WriteLine($"""Fehler beim speichern des Datums: {istAusgabe}!""");
            }
            bool istFix = User.GetIstFixInput();
            if (!Inputs.TryAdd("""IstFix""", istFix))
            {
                Console.WriteLine($"""Fehler beim speichern des Datums: {istFix}!""");
            }

            return Inputs;
        }
    }
}