using Buchhaltung.Common;
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

            if (!File.Exists(Common.Common.FixCostFilepath))
            {
                File.WriteAllText(Common.Common.FixCostFilepath, "", new UTF8Encoding());
            }

            if (!File.Exists(Common.Common.CurrMonthFilepath))
            {
                string fixCostData = "";
                if (File.ReadAllLines(Common.Common.FixCostFilepath).Length > 1)
                {
                    var fixCostEntries = Common.Common.GetEntries(Common.Common.FixCostFilepath);
                    fixCostData = JsonSerializer.Serialize(fixCostEntries, Common.Common.JsonOptions);
                }
                File.WriteAllText(Common.Common.CurrMonthFilepath, fixCostData, new UTF8Encoding());
            }

            Console.WriteLine("""Initialisierung erfolgreich abgeschlossen.""");
        }

        public static int TestMain()
        {
            Console.WriteLine(File.ReadAllLines(Common.Common.FixCostFilepath).Length);

            return 0;
        }

        public static int Main()
        {
            Init();
            _ = new MainMenu();

            return 0;
        }
    }
}