using CommandLine;

namespace NameDay
{
    public class Options
    {
        [Option('d', "date", Required = true, HelpText = "Date should be in format d.M. for example 3.11.")]
        public string Date { get; set; }

        [Option('f', "filepath", Required = false, HelpText = "Filepath to csv-file.")]
        public string Filepath { get; set; }
    }
}
