using System;
using System.ComponentModel;
using System.IO.Enumeration;

namespace Buchhaltung.Common
{
    public class FixCost
    {
        private static string filePath = "/home/ben/cs_workspace/Buchhaltung/Src/FixCost_data.json";
        public static string filename = filePath.Substring(filePath.LastIndexOf("/"));

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

        public static void Save(Entry fixCostEntry)
        {
            if (File.Exists(filename))
            {
                Entry.Save(filename, fixCostEntry);
            }
            Console.WriteLine("Eintrag wurde erfolgreich zu Fix-Kosten hinzugefügt.");
        }
    }
}