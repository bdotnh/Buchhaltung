using System;

namespace Buchhaltung.Common
{
    public class MonthMenu() 
        : Menu("Monats-Auswahl", ["Exit", "Monat ändern"]);

    public class DisplayMonth
    {
        public DisplayMonth(string month = "")
        {
            if (month == "")
            {
                month = Common.GetCurrentMonth();
            }

            Console.WriteLine($"Ausgewählter Monat: {month}.");
            _ = new MonthMenu();
        }
    }
}

