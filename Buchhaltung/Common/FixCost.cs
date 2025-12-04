using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;



namespace Buchhaltung.Common
{
    public class FixCost
    {
        private static string filePath = Common.fixCostFilepath;
        private static string filename = filePath.Substring(filePath.LastIndexOf("/"));

        public FixCost()
        {
            Show();
            Menu menu = new Menu(3);
        }

        public static void Show()
        {
            if (File.Exists(filePath) && File.ReadAllLines(filePath).Length > 1)
            {
                Month.Show(filename.Substring(0, filename.LastIndexOf("_")));
            }
        }

        public static void Save(Entry entry)
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
        }
    }
}