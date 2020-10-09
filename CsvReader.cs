using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvReader
{
    class CsvReader
    {
        private string _csvFilePath;

        public CsvReader(string csvFilePath)
        {
            this._csvFilePath = csvFilePath;
        }

        public void ReadFirstNCountriesArr()
        {
            Console.Write("\nHow many top countries do you want to list? ");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count))
            {
                Console.Write("Not a number, try again: ");
            }

            Country[] countries = new Country[count];
            StreamReader sr = new StreamReader(_csvFilePath);

            // read header line
            sr.ReadLine();

            for (int i = 0; i < count; i++)
            {
                string csvLine = sr.ReadLine();
                if (string.IsNullOrEmpty(csvLine)) break;
                countries[i] = ReadCountryFromCsvLine(csvLine);
            }
            sr.Close();

            // remove all empty items
            countries = countries.Where(x => !Country.IsNull(x)).ToArray();

            Console.WriteLine(PopulationFormatter.PrintCountries(countries));
        }

        public void ReadAllCountriesLst()
        {
            List<Country> countries = new List<Country>();

            StreamReader sr = new StreamReader(_csvFilePath);

            // read header line
            sr.ReadLine();

            string csvLine;
            while ((csvLine = sr.ReadLine()) != null)
            {
                countries.Add(ReadCountryFromCsvLine(csvLine));
            }
            sr.Close();

            Console.WriteLine(PopulationFormatter.PrintCountries(countries));
        }

        public void FindPopulationByCodeDctnr()
        {
            Dictionary<string, Country> countries = new Dictionary<string, Country>();
            StreamReader sr = new StreamReader(_csvFilePath);

            // read header line
            sr.ReadLine();

            string csvLine;
            while ((csvLine = sr.ReadLine()) != null)
            {
                Country countryToFill = ReadCountryFromCsvLine(csvLine);
                countries.Add(countryToFill.Code, countryToFill);
            }
            sr.Close();

            Console.Write("Which country code do you want to look up? ");
            string countryCode = Console.ReadLine();
            Country countryToSearch;
            bool existsCountry = countries.TryGetValue(countryCode, out countryToSearch);
            while (!existsCountry)
            {
                Console.WriteLine($"Sorry, there is no country with code {countryCode}");
                Console.Write("Try again: ");
                countryCode = Console.ReadLine();
                existsCountry = countries.TryGetValue(countryCode, out countryToSearch);
            }
            Console.WriteLine(PopulationFormatter.PrintCountries(countryToSearch.Name, countryToSearch.Population, 1));
        }

        private Country ReadCountryFromCsvLine(string csvLine)
        {
            string[] parts = csvLine.Split(',');

            string name;
            string code;
            string region;
            string popText;

            switch (parts.Length)
            {
                case 4:
                    name = parts[0];
                    code = parts[1];
                    region = parts[2];
                    popText = parts[3];
                    break;
                case 5:
                    name = parts[0] + ", " + parts[1];
                    name = name.Replace("\"", "").Trim();
                    code = parts[2];
                    region = parts[3];
                    popText = parts[4];
                    break;
                default:                    
                    throw new Exception($"Cannot parse country from csvLine: {csvLine}");                    
            }

            int.TryParse(popText, out int population);
            return new Country(name, code, region, population);
        }
    }
}
