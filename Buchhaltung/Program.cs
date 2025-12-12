using Buchhaltung.Common;

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

            Console.WriteLine("""Initialisierung erfolgreich abgeschlossen.""");
        }

        public static int Test()
        {
            Year year = new("");
            year.ShowAllMonthTotals();
            year.ShowYearTotals();

            return 0;
        }

        public static int Main()
        {
            Test();

            Init();
            _ = new MainMenu();

            return 0;
        }
    }
}