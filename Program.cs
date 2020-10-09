using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace CsvReader
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filePath = "c:\\Users\\4210055\\projects\\CsvReader\\temp\\Pop by Largest Final.csv";
            CsvReader csvReader = new CsvReader(filePath);
            bool repeat = true;            

            while (repeat) {
                char action = AskForAction();
                switch (action)
                {
                    case '1':
                        csvReader.ReadFirstNCountriesArr(); ;
                        break;
                    case '2':
                        csvReader.ReadAllCountriesLst();
                        break;
                    case '3':
                        csvReader.FindPopulationByCodeDctnr();                        
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
                repeat = AskForAnotherRound();
            }
        }

        private static char AskForAction()
        {
            Console.WriteLine("\nChoose option:");
            Console.WriteLine("1 - List first n countries");
            Console.WriteLine("2 - List all countries");
            Console.WriteLine("3 - Find population based on the country code");
            Console.WriteLine("Note: Countries are ordered from the most populated to the least populated one");
            char action;
            while (!char.TryParse(Console.ReadLine(), out action))
            {
                Console.Write("Invalid input, try again: ");
            }

            while (action < 49 || action > 51)
            {
                Console.Write("\nInvalid input, try again: ");
                action = Console.ReadKey().KeyChar;
                continue;
            }
            return action;
        }

        private static bool AskForAnotherRound()
        {
            Console.Write("\nDo you want to search again? [y/n] ");
            char repeatChar = Console.ReadKey().KeyChar;
            while (repeatChar != 'y')
            {
                if (repeatChar != 'n')
                {
                    Console.Write("\nNot a valid value, try again: [y/n] ");
                    repeatChar = Console.ReadKey().KeyChar;
                    continue;
                }
                return false;
            }
            return true;
        }
    }
}
