using Buchhaltung.Common;
using System.IO;

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