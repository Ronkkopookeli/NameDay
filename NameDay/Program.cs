using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NameDay
{
    public class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => {
                    ValidateOptions(opts);
                    Console.Write(Filereader.GetNamesFromFileByDate(opts.Date, opts.Filepath));
            });
        }

        // Returns bool instead of void to ease testing.
        public static bool ValidateOptions(Options opts)
        {
            var match = Regex.Match(opts.Date,
                                    @"^([1-9]|[12][0-9]|3[01])([.])([1-9]|[0-9]|1[02])([.])$");
            if (!match.Success)
            {
                throw new Exception(
                    $@"Bad date: {opts.Date}.
Expected format: d.M. for example 3.11.");
            }

            if (opts.Filepath != null)
            {
                var exists = File.Exists(opts.Filepath);
                if (!exists)
                {
                    throw new Exception(
                        $"File ({opts.Filepath}) does not exist.");
                }
            }
            return true;
        }
    }
}
