using Buchhaltung.Common;
using System.IO;
using System.Text;

namespace Buchhaltung
{
    class Program
    {
        public static void Init()
        {
            Console.WriteLine("Initialisierung...");

            if (!IsDirectoryExisting())
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Src");
            }
            string currDir = Directory.GetCurrentDirectory() + "/Src/";
            
            string currentMonthFn = currDir + Common.Common.GetCurrentMonth() + "_data.json";
            if (!File.Exists(currentMonthFn))
            {
                File.WriteAllText(currentMonthFn, "", new UTF8Encoding()); 
            }

            string fixCostFn = currDir + "FixCost_data.json";
            if (!File.Exists(fixCostFn))
            {
                File.WriteAllText(fixCostFn, "", new UTF8Encoding());
            }

            Console.WriteLine("Initialisierung erfolgreich abgeschlossen.");
        }

        private static bool IsDirectoryExisting()
        {
            if (Directory.Exists(Directory.GetCurrentDirectory() + "/Src"))
            {
                return true;
            }

            return false;
        }

        public static int Main()
        {
            Init();
            _ = new MainMenu();

            return 0;
        }
    }
}   