// using System.ComponentModel.Design;

// namespace Buchhaltung.Common
// {
//     public class Menu
//     {
//         private static readonly string[] MenuChoices =
//         [
//             "Exit",
//             "Neuer Eintrag",
//             "Monats-Übersicht",
//             "Fix Kosten",
//         ];

//         private void ShowMenu()
//         {
//             Console.WriteLine("-- Menü --");
//             for (int i = 0; i < MenuChoices.Length; i++)
//             {
//                 Console.WriteLine($"{i}. {MenuChoices[i]}.");
//             }
//         }

//         protected bool IsMenuChoiceValid(string userInput)
//         {
//             if (!int.TryParse(userInput, out int temp))
//             {
//                 Console.WriteLine("Eingabe muss nummer sein!");
//                 return false;
//             }

//             if (temp < 0 || temp > MenuChoices.Length)
//             {
//                 Console.WriteLine($"Nummer darf nicht kleiner 0 oder größer als {MenuChoices.Length} sein!");
//                 return false;
//             }

//             return true;
//         }

//         public Menu()
//         {
//             ShowMenu();
//             string? userInput = "";
//             bool isValid = false;
//             while (!isValid)
//             {
//                 userInput = Console.ReadLine();
//                 if (userInput == "0")
//                 {
//                     Environment.Exit(0);
//                 }
//                 if (userInput != "" && IsMenuChoiceValid(userInput))
//                 {
//                     isValid = true;
//                     break;
//                 }
//             }

//             int menuChoice = Convert.ToInt32(userInput);
//             switch (menuChoice)
//             {
//                 case 0:
//                     Environment.Exit(0);
//                     break;
//                 case 1:
//                     Entry.Save();
//                     break;
//                 case 2:
//                     Month.Show("");
//                     break;
//             }
//         }
//     }
// }