using System.Collections.Generic;
using Xunit;
using System.Linq;
using System.IO;
using System;

namespace NameDay.Tests
{
    public class FilereaderTests
    {
        private readonly List<NamesOfDay> NamesAndDates = new List<NamesOfDay>() {
            new NamesOfDay
            {
                Date = "30.11.",
                Names = "Antti, Atte"
            }
        };

        [Fact]
        public void GetNamesByDate_DateNotOnList_ReturnsNotFound()
        {
            var result = Filereader.GetNamesByDate(NamesAndDates.AsQueryable(), "11.11.");
            Assert.Equal("No names found on given date.", result);
        }

        [Fact]
        public void GetNamesByDate_DateOnList_ReturnsName()
        {
            var result = Filereader.GetNamesByDate(NamesAndDates.AsQueryable(), "30.11.");
            Assert.Equal("Antti, Atte", result);
        }

        [Fact]
        public void GetNamesFromFileByDate_FileFormatWrong_ThrowsException()
        {
            Exception ex = Assert.Throws<Exception>(() =>
                Filereader.GetNamesFromFileByDate("2.9.",
                                                  Path.Combine(
                                                      Directory.GetCurrentDirectory(), 
                                                      "NameDay.pdb")));
            Assert.Equal("File is in incorrect format.", ex.Message);
        }

        [Fact]
        public void GetNamesFromFileByDate_FileNotFound_ThrowsException()
        {
            Exception ex = Assert.Throws<Exception>(() =>
                Filereader.GetNamesFromFileByDate("2.9.",
                                                  Path.Combine(
                                                      Directory.GetCurrentDirectory(),
                                                      "asdasd.dsa")));
            Assert.Equal("File not found.", ex.Message);
        }

        [Fact]
        public void GetNamesFromFileByDate_FileFound_ReturnsName()
        {
            
           var result = 
                Filereader.GetNamesFromFileByDate("2.9.",
                                                  Path.Combine(
                                                      Directory.GetCurrentDirectory(),
                                                      "nimet.csv"));
            Assert.Equal("Sinikka, Sini, Justus", result);
        }
    }
}
