using System;
using System.Buffers.Text;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;

namespace Buchhaltung.Common
{
    public class Entry
    {
        public static string GetDatumInput()
        {
            string userInput = "";
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("Datum: ");
                userInput = Console.ReadLine();
                if (!String.IsNullOrEmpty(userInput))
                {
                    userInput.Replace(",", ".");
                    if (userInput.IndexOf('.') == 1)
                    {
                        userInput.Insert(0, "0");
                    }

                    if (userInput.LastIndexOf('.') == 4)
                    {
                        userInput.Insert(3, "0");
                    }

                    if (userInput.Length == 10 && userInput.Count(f => f == '.') == 2)
                    {
                        isVaild = true;
                        break;
                    }
                }
            }

            return userInput; // 00.00.0000
        }

        public static float GetBetragInput()
        {
            string betragInput = "";
            float betragValue = -1.0f;
            bool isVaild = false;
            while (!isVaild)
            {
                Console.Write("Betrag: ");
                betragInput = Console.ReadLine();
                betragInput.Replace(',', '.');
                if (float.TryParse(betragInput, out betragValue))
                {
                    if (betragValue > 0.0f)
                    {
                        isVaild = true;
                        break;
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
                    break;
                }
                else if (userInput == "N" || userInput == "n")
                {
                    istFix = false;
                    isValid = true;
                    break;
                }
            }

            return istFix;
        }

        public string[] Categories = ["Datum", "Betrag", "Geschäft", "EinAusgabe", "IstFix"];
        public string Datum { get; set; }
        public float Betrag { get; set; }
        public string Geschäft { get; set; }
        public bool IstAusgabe { get; set; }
        public bool IstFix { get; set; }

        public Entry()
        {
        }

        public static void Save()
        {
            Entry entry = new Entry();
            /* For real input
            entry.Datum = GetDatumInput();
            entry.Betrag = GetBetragInput();
            entry.Geschäft = GetGeschäftInput();
            entry.IstAusgabe = GetIstAusgabeInput();
            entry.IstFix = GetIstFixInput();
            */
            entry.Datum = "30.11.2025";
            entry.Betrag = 12.34f; 
            entry.Geschäft = "Aldi";
            entry.IstAusgabe = true;
            entry.IstFix = false; 

            string filename = $"{Common.GetCurrentMonth()}_data.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(entry, options);
            File.WriteAllText(filename, jsonString, Encoding.UTF8);
        }
    }
}