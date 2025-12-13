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

        private readonly string[] promptMonthComparisonMenu =
        [
            "Exit",
            "Monate auswählen/ändern"
        ];

        private readonly string[] promptYearComparisonMenu =
        [
            "Exit",
            "Jahre auswählen/ändern"
        ];

        public Menu(int menu)
        {
            if (menu == (int)EMenu.Month)
            {
                InitMenuPrompt(menu);
                MonthMenu();
            }
            else if (menu == (int)EMenu.Year)
            {

                InitMenuPrompt(menu);
                YearMenu();
            }
            else if (menu == (int)EMenu.FixCosts)
            {

                InitMenuPrompt(menu);
                FixCostMenu();
            }
            else if (menu == (int)EMenu.MonthComparison)
            {
                MonthComparisonMenu();
                InitMenuPrompt(menu);
            }
            else if (menu == (int)EMenu.YearComparison)
            {
                YearComparisonMenu();
                InitMenuPrompt(menu);
            }
        }

        private static void YearComparisonMenu()
        {
            string y1 = User.GetYearInput("Jahr 1 eingeben: ");
            string y2 = User.GetYearInput("Jahr 2 eingeben: ");

            YearComparison yearComparison = new(y1, y2);
            yearComparison.ShowComparison();
        }

        private static void MonthComparisonMenu()
        {
            string m1 = User.GetMonthInput("Monat 1 eingeben: ");
            string m2 = User.GetMonthInput("Monat 2 eingeben: ");

            MonthComparison monthComparison = new(m1, m2);
            monthComparison.ShowComparison();
        }

        private static void YearMenu()
        {
            Year year = new("");
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
                    new Year(newDate).Show();
                }
            }
        }

        private static void MonthMenu()
        {
            Month month = new("");
            string newDate = "";
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
                    newDate = User.GetMonthInput("Monat: ");
                    new Month(newDate).Show();
                }
                else if (menuChoice == (int)EMonthMenu.DeleteEntries)
                {
                    month = new Month(newDate);
                    List<int> entryChoices = User.GetNumsInput("Zum löschen mehrerer Einträge Nummer mit ',' trennen: ");
                    List<Entry> entries = month.SelectEntries(entryChoices);
                    month.DeleteEntries(entries);
                }
            }
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
            Console.WriteLine("-- Menü --");
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
            else if (menu == (int)EMenu.MonthComparison)
            {
                ShowPrompts(promptMonthComparisonMenu);
            }
            else if (menu == (int)EMenu.YearComparison)
            {
                ShowPrompts(promptYearComparisonMenu);
            }
        }
    }
}
