using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NameDay
{
    public class Filereader
    {
        public static string GetNamesFromFileByDate(string date, string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "nimet.csv");
            }

            try
            {
                using (var reader = new StreamReader(path))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        csv.Configuration.HasHeaderRecord = false;
                        csv.Configuration.Delimiter = ";";
                        try
                        {
                            var records = csv.GetRecords<NamesOfDay>();
                            return GetNamesByDate(records, date);
                        }
                        catch (CsvHelper.MissingFieldException)
                        {
                            throw new Exception("File is in incorrect format.");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new Exception("File not found.");
            }
        }

        // Made public to ease testing.
        public static string GetNamesByDate(IEnumerable<NamesOfDay> namesAndDates,
                                             string date)
        {
            var names = namesAndDates.Where(n => n.Date == date).Select(n => n.Names).FirstOrDefault();
            
            if (!string.IsNullOrEmpty(names))
            {
                return names;
            }
            else
            {
                return "No names found on given date.";
            }
        }
    }
}
