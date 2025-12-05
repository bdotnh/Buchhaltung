using Buchhaltung.Common;
using System.Text;

namespace Buchhaltung
{
    class Program
    {
        public static void Init()
        {
            Console.WriteLine("Initialisierung...");

            string currDir = Directory.GetCurrentDirectory() + "/Src/";
            if (!Directory.Exists(currDir))
            {
                Directory.CreateDirectory(currDir);
            }
            
            if (!File.Exists(Common.Common.currMonthFilepath))
            {
                File.WriteAllText(Common.Common.currMonthFilepath, "", new UTF8Encoding()); 
            }

            if (!File.Exists(Common.Common.fixCostFilepath))
            {
                File.WriteAllText(Common.Common.fixCostFilepath, "", new UTF8Encoding());
            }

            Console.WriteLine("Initialisierung erfolgreich abgeschlossen.");
        }

        public static int Main()
        {
            Init();
            _ = new MainMenu();

            return 0;
        }
    }
}   