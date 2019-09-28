using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;
using NameDay;
using Xunit;
using System.Linq;
using System.IO;

namespace NameDay.Tests
{
    public class OptionsTests
    {
        [Fact]
        public void ValidateOptions_DateWithZeroes_ThrowsError()
        {
            Options options = new Options()
            {
                Date = "01.01.",
                Filepath = null
            };

            Exception ex = Assert.Throws<Exception>(() => 
                Program.ValidateOptions(options));
            Assert.Equal($@"Bad date: {options.Date}.
Expected format: d.M. for example 3.11.", ex.Message);
        }

        [Fact]
        public void ValidateOptions_DateWithTooBigDay_ThrowsError()
        {
            Options options = new Options()
            {
                Date = "32.01.",
                Filepath = null
            };

            Exception ex = Assert.Throws<Exception>(() =>
                Program.ValidateOptions(options));
            Assert.Equal($@"Bad date: {options.Date}.
Expected format: d.M. for example 3.11.", ex.Message);
        }

        [Fact]
        public void ValidateOptions_DateWithTooBigMonth_ThrowsError()
        {
            Options options = new Options()
            {
                Date = "30.13.",
                Filepath = null
            };

            Exception ex = Assert.Throws<Exception>(() =>
                Program.ValidateOptions(options));
            Assert.Equal($@"Bad date: {options.Date}.
Expected format: d.M. for example 3.11.", ex.Message);
        }

        [Fact]
        public void ValidateOptions_DateWithBadFormat1_ThrowsError()
        {
            Options options = new Options()
            {
                Date = "30.12",
                Filepath = null
            };

            Exception ex = Assert.Throws<Exception>(() =>
                Program.ValidateOptions(options));
            Assert.Equal($@"Bad date: {options.Date}.
Expected format: d.M. for example 3.11.", ex.Message);
        }

        [Fact]
        public void ValidateOptions_DateWithBadFormat2_ThrowsError()
        {
            Options options = new Options()
            {
                Date = "30/12",
                Filepath = null
            };

            Exception ex = Assert.Throws<Exception>(() =>
                Program.ValidateOptions(options));
            Assert.Equal($@"Bad date: {options.Date}.
Expected format: d.M. for example 3.11.", ex.Message);
        }

        [Fact]
        public void ValidateOptions_FileNotFound_ThrowsError()
        {
            Options options = new Options()
            {
                Date = "30.12.",
                Filepath = "foo.bar"
            };

            Exception ex = Assert.Throws<Exception>(() =>
                Program.ValidateOptions(options));
            Assert.Equal($"File ({options.Filepath}) does not exist.", ex.Message);
        }

        [Fact]
        public void ValidateOptions_GoodDate_ReturnsTrue()
        {
            Options options = new Options()
            {
                Date = "30.12.",
                Filepath = null
            };

            var result = Program.ValidateOptions(options);
            Assert.True(result);
        }

        [Fact]
        public void ValidateOptions_GoodDateAndFilepath_ReturnsTrue()
        {
            Options options = new Options()
            {
                Date = "30.12.",
                Filepath = Path.Combine(Directory.GetCurrentDirectory(),
                                        "nimet.csv")
            };

            var result = Program.ValidateOptions(options);
            Assert.True(result);
        }
    }
}
