using Utf8Json.Formatters;

namespace Buchhaltung.Common
{
    public class Month
    {
        public static string _srcPath = "/home/ben/cs_workspace/Buchhaltung/Src";
        private static List<string> allFiles = Common.GetFilenamesInDir(_srcPath);
        private static List<string> allMonthFiles = allFiles.FindAll(x => (x.Contains("_data.json")));
        private string[] promptMonthMenu =
        [
            "Exit",
            "Monat ändern"
        ];


        public Month()
        {
            Show("");
            Menu menu = new Menu(2);

        }

        public static string GetMonthUserInput()
        {
            string userInput = "";
            bool isValid = false;
            while (!isValid)
            {
                Console.WriteLine("Monat: ");
                userInput = Console.ReadLine();
                if (allMonthFiles.Contains(userInput))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine($"Monat: {userInput} ist nicht gespeichert. Sollen alle gespeicherten Monate angezeigt werden? J/n: ");
                    userInput = Console.ReadLine();
                    if (userInput == "J" | userInput == "j")
                    {
                        Console.WriteLine("Alle gespeicherten Monate: ");
                        foreach (string filename in allMonthFiles)
                        {
                            Console.Write($"{filename.Substring(0, filename.LastIndexOf("_"))}; ");
                        }
                    }
                }
            }
            return userInput;
        }

        public static void Show(string monthDate)
        {
            if (monthDate == "")
            {
                monthDate = Common.GetCurrentMonth();
            }
            string filename = Directory.GetCurrentDirectory() + "/Src/" + monthDate + "_data.json";
            List<Entry> entries = Common.GetEntries(filename);
            Console.WriteLine(" ID   |     Datum     |   Betrag  |    Geschäft    | IstAusgabe | IstFix |");
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($" {i}    |   {entries[i].Datum}  |   {entries[i].Betrag}    |    {entries[i].Geschäft}   |   {entries[i].IstAusgabe}     |   {entries[i].IstFix}");
            }
        }
    }
}