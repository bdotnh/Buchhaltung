using System;

namespace Buchhaltung.Common
{
    public enum EMonthMenu
    {
        Exit = 0,
        ChangeMonth,
        DeleteEntries,
    }
    public enum EFixMenu
    {
        Exit = 0,
        DeleteEntry,
    }

    public enum EYearMenu
    {
        Exit = 0,
        ChangeYear
    }

    public class Menu
    {
        public static string[] menuPrompts = [];

        private readonly string[] promptEntryMenu =
        [
            "Exit"
        ];

        private readonly string[] promptMonthMenu =
        [
            "Exit",
            "Monat ändern",
            "Einträge löschen"
        ];

        private readonly string[] promptYearMenu =
        [
            "Exit",
            "Jahr ändern",
        ];

        private readonly string[] promptFixMenu =
        [
            "Exit",
            "Fixkosten-Einträge löschen"
        ];

        public Menu(int menu, Month month = null, Year year = null) 
        {
            if (menu == (int)EMenu.Month)
            {
                InitMenuPrompt(menu);
                MonthMenu(month);
            }
            else if (menu == (int)EMenu.Year)
            {

                InitMenuPrompt(menu);
                YearMenu(year);
            }
            else if (menu == (int)EMenu.FixCosts)
            {

                InitMenuPrompt(menu);
                FixCostMenu();
            }
            else if (menu == (int)EMenu.MonthComparison)
            {
                if (User.GetAllSavedMonths().Count < 2)
                {
                    Console.WriteLine("Es sind noch keine 2 Monate gespeichert. Daher ist ein Vergleich nicht möglich.");
                }
                else
                {
                    string m1 = User.GetMonthInput("Monat 1 eingeben: ");
                    string m2 = User.GetMonthInput("Monat 2 eingeben: ");

                    MonthComparison monthComparison = new(m1, m2);
                    monthComparison.ShowComparison();
                }
            }
            else if (menu == (int)EMenu.YearComparison)
            {
                if (User.GetAllSavedYears().Count < 2)
                {
                    Console.WriteLine("Es sind noch keine 2 Jahre gespeichert. Daher ist ein Vergleich nicht möglich.");
                }
                else
                {
                    string y1 = User.GetYearInput("Jahr 1 eingeben: ");
                    string y2 = User.GetYearInput("Jahr 2 eingeben: ");

                    YearComparison yearComparison = new(y1, y2);
                    yearComparison.ShowComparison();
                }
            }
        }

        private static Year YearMenu(Year year)
        {
            string message = "Eingabe: ";
            int menuChoice = User.GetInputNumber(message);
            if (menuChoice == (int)EYearMenu.Exit)
            {
                _ = new MainMenu();
            }
            else
            {
                if (menuChoice == (int)EYearMenu.ChangeYear)
                {
                    string newDate = User.GetYearInput("Jahr: ");
                    year = new Year(newDate);
                    year.Show();
                    new Menu((int)EMenu.Year, null, year);
                }
            }

            return year;
        }

        private static Month MonthMenu(Month month)
        {
            string message = "Eingabe: ";
            int menuChoice = User.GetInputNumber(message);
            if (menuChoice == (int)EMonthMenu.Exit)
            {
                _ = new MainMenu();
            }
            else
            {
                if (menuChoice == (int)EMonthMenu.ChangeMonth)
                {
                    string newDate = User.GetMonthInput("Monat: ");
                    month = new Month(newDate);
                    month.Show();
                    new Menu((int)EMenu.Month, month);
                }
                else if (menuChoice == (int)EMonthMenu.DeleteEntries)
                {
                    List<int> entryChoices = User.GetNumsInput("Zum löschen mehrerer Einträge Nummer mit ',' trennen: ");
                    List<Entry> entries = month.SelectedEntries(entryChoices);
                    month.DeleteEntries(entries);
                }
            }

            return month;
        }

        private static void FixCostMenu()
        {
            FixCost fixCost = new();
            string message = "Eingabe: ";
            int menuChoice = User.GetInputNumber(message);
            if (menuChoice == (int)EFixMenu.Exit)
            {
                _ = new MainMenu();
            }
            else
            {
                if (menuChoice == (int)EFixMenu.DeleteEntry)
                {

                    List<int> entryChoices = User.GetNumsInput("Zum löschen mehrerer Einträge Nummer mit ',' trennen: ");
                    List<Entry> entries = fixCost.SelectEntries(entryChoices);
                    fixCost.DeleteEntries(entries);
                }
            }
        }

        public static void ShowPrompts(string[] menuPrompts)
        {
            Console.WriteLine("\n-- Menü --");
            for (int i = 0; i < menuPrompts.Length; i++)
            {
                Console.WriteLine($"{i}. {menuPrompts[i]}.");
            }
        }

        private void InitMenuPrompt(int menu)
        {
            if (menu == (int)EMenu.NewEntry)
            {
                ShowPrompts(promptEntryMenu);
            }
            else if (menu == (int)EMenu.Month)
            {
                ShowPrompts(promptMonthMenu);
            }
            else if (menu == (int)EMenu.Year)
            {
                ShowPrompts(promptYearMenu);
            }
            else if (menu == (int)EMenu.FixCosts)
            {
                ShowPrompts(promptFixMenu);
            }
        }
    }
}
