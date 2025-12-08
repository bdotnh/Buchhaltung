using Buchhaltung.Common;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Buchhaltung
{
    class Program
    {
        public static void Init()
        {
            Console.WriteLine("""Initialisierung...""");

            string currDir = Directory.GetCurrentDirectory() + """/Src/""";
            if (!Directory.Exists(currDir))
            {
                Directory.CreateDirectory(currDir);
            }
 
            if (!File.Exists(Common.Common.fixCostFilepath))
            {
                File.WriteAllText(Common.Common.fixCostFilepath, "", new UTF8Encoding());
            }

            if (!File.Exists(Common.Common.currMonthFilepath))
            {
                string fixCostData = "";
                if (File.ReadAllLines(Common.Common.fixCostFilepath).Length > 1)
                {
                    var fixCostEntries = Common.Common.GetEntries(Common.Common.fixCostFilepath);
                    fixCostData = JsonSerializer.Serialize(fixCostEntries, Common.Common.JsonOptions);
                }
                File.WriteAllText(Common.Common.currMonthFilepath, fixCostData, new UTF8Encoding()); 
            }

            Console.WriteLine("""Initialisierung erfolgreich abgeschlossen.""");
        }

        public static int TestMain()
        {
            Console.WriteLine(File.ReadAllLines(Common.Common.fixCostFilepath).Length);

            return 0;
        }

        public static int Main()
        {
            Init();
            int userInput = User.GetInputNumber("1. Test, 2. Hauptmenü");
            if (userInput == 1)
            {
                TestMain();
            } else
            {
                _ = new MainMenu();
            } 

            return 0;
        }
    }
}   