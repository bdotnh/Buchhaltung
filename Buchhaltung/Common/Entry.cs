using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Buchhaltung.Common
{
    public class Entry
    {
        public string[] Categories = ["Datum", "Betrag", "Geschäft", "EinAusgabe", "IstFix"];
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

            if (entry.IstFix)
            {
                FixCost.Save(entry);
            }
        }

        public static Dictionary<string, object> GetInputs()
        {
            Dictionary<string, object> Inputs = new Dictionary<string, object>
            {
                { "Datum", "" },
                { "Betrag", 0.0f },
                { "Geschäft", "" },
                { "IstAusgabe", true },
                { "IstFix", false }
            };

            string datum = GetDatumInput();
            if (!Inputs.TryAdd("Datum", datum))
            {
                Console.WriteLine($"Fehler beim speichern des Datums: {datum}!");
            }
            float betrag = GetBetragInput();
            if (!Inputs.TryAdd("Betrag", betrag))
            {
                Console.WriteLine($"Fehler beim speichern des Betrags: {betrag}!");
            }
            string geschäft = GetGeschäftInput();
            if (!Inputs.TryAdd("Geschäft", geschäft))
            {
                Console.WriteLine($"Fehler beim speichern des Datums: {geschäft}!");
            }
            bool istAusgabe = GetIstAusgabeInput();
            if (!Inputs.TryAdd("IstAusgabe", istAusgabe))
            {
                Console.WriteLine($"Fehler beim speichern des Datums: {istAusgabe}!");
            }
            bool istFix = GetIstFixInput();
            if (!Inputs.TryAdd("IstFix", istFix))
            {
                Console.WriteLine($"Fehler beim speichern des Datums: {istFix}!");
            }

            return Inputs;
        }

        public static string GetDatumInput()
        {
            string? userInput = "";
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("Datum: ");
                userInput = Console.ReadLine();
                if (!String.IsNullOrEmpty(userInput))
                {
                    _ = userInput.Replace(",", ".");
                    if (userInput.IndexOf('.') == 1)
                    {
                        _ = userInput.Insert(0, "0");
                    }

                    if (userInput.LastIndexOf('.') == 4)
                    {
                        _ = userInput.Insert(3, "0");
                    }

                    if (userInput.Length == 10 && userInput.Count(f => f == '.') == 2)
                    {
                        isVaild = true;
                    }
                }
            }

            return userInput; // 00.00.0000
        }

        public static float GetBetragInput()
        {
            string? betragInput;
            float betragValue = -1.0f;
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("Betrag: ");
                betragInput = Console.ReadLine();
                _ = betragInput.Replace(',', '.');
                if (float.TryParse(betragInput, out betragValue))
                {
                    if (betragValue > 0.0f)
                    {
                        isVaild = true;
                    }
                }
            }

            return betragValue;
        }

        public static string GetGeschäftInput()
        {
            string GeschäftInput = "";
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("Geschäft: ");
                GeschäftInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(GeschäftInput) && GeschäftInput.Length > 2)
                {
                    isVaild = true;
                    break;
                }
            }

            return GeschäftInput;
        }

        public static bool GetIstAusgabeInput()
        {
            bool istAusgabe = false;
            bool isValid = false;
            string userInput = "";
            while (!isValid)
            {
                Console.WriteLine("Ausgabe? J/n: ");
                userInput = Console.ReadLine();
                if (userInput == "J" || userInput == "j")
                {
                    istAusgabe = true;
                    isValid = true;
                    break;
                }
                else if (userInput == "N" || userInput == "n")
                {
                    istAusgabe = false;
                    isValid = true;
                    break;
                }
            }

            return istAusgabe;
        }

        public static bool GetIstFixInput()
        {
            bool istFix = false;
            bool isValid = false;
            string userInput = "";
            while (!isValid)
            {
                Console.Write("Zu Fixkosten hinzufügen? J/n: ");
                userInput = Console.ReadLine();
                if (userInput == "J" || userInput == "j")
                {
                    istFix = true;
                    isValid = true;
                }
                else if (userInput == "N" || userInput == "n")
                {
                    istFix = false;
                    isValid = true;
                }
            }

            return istFix;
        }


    }
}