using CsvHelper.Configuration.Attributes;

namespace NameDay
{
    public class NamesOfDay
    {
        [Index(0)]
        public string Date { get; set; }
        [Index(1)]
        public string Names { get; set; }
    }
}
