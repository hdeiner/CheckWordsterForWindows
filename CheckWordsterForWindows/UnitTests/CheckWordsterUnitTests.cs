using System;
using Xunit;
using CheckWordsterForWindows.Services;

namespace CheckWordsterForWindows.UnitTests
{
    public class CheckWordsterUnitTests
    {
        [Fact]
        public void TestNullString()
        {
            var ex = Assert.Throws<Exception>(() => new CheckWordster(""));
            Assert.Equal("CheckWordster - Null number", ex.Message);
        }

        [Fact]
        public void TestInvalidCharacters()
        {
            var ex = Assert.Throws<Exception>(() => new CheckWordster("Person2Person"));
            Assert.Equal("CheckWordster - Invalid characters", ex.Message);
        }

        [Fact]
        public void TestInvalidSignedNumber1()
        {
            var ex = Assert.Throws<Exception>(() => new CheckWordster("-200.+00"));
            Assert.Equal("CheckWordster - Invalid signed number", ex.Message);
        }

        [Fact]
        public void TestInvalidSignedNumber2()
        {
            var ex = Assert.Throws<Exception>(() => new CheckWordster("++200.00"));
            Assert.Equal("CheckWordster - Invalid signed number", ex.Message);
        }

        [Fact]
        public void TestSignedNumber()
        {
            var ex = Assert.Throws<Exception>(() => new CheckWordster("-200.00"));
            Assert.Equal("CheckWordster - Signed number", ex.Message);
        }

        [Fact]
        public void TestInvalidFormat1()
        {
            var ex = Assert.Throws<Exception>(() => new CheckWordster("200,000,0000"));
            Assert.Equal("CheckWordster - Invalid format", ex.Message);
        }

        [Fact]
        public void TestInvalidFormat2()
        {
            var ex = Assert.Throws<Exception>(() => new CheckWordster("200.000"));
            Assert.Equal("CheckWordster - Invalid format", ex.Message);
        }

        [Fact]
        public void TestInvalidFormat3()
        {
            var ex = Assert.Throws<Exception>(() => new CheckWordster("200.00.00"));
            Assert.Equal("CheckWordster - Invalid format", ex.Message);
        }

        [Fact]
        public void TestDigitWordExtraction1()
        {
            var cw = new CheckWordster("20");
            Assert.Equal("twenty", cw.WordsForUpTo1000("20"));
        }

        [Fact]
        public void TestWordForDigits1()
        {
            var cw = new CheckWordster("20");
            Assert.Equal("Twenty", cw.GetWords());
        }

        [Fact]
        public void TestDigitWordExtraction2()
        {
            var cw = new CheckWordster("25");
            Assert.Equal("twenty five", cw.WordsForUpTo1000("25"));
        }

        [Fact]
        public void TestWordForDigits2()
        {
            var cw = new CheckWordster("25");
        Assert.Equal("Twenty five", cw.GetWords());
        }

        [Fact]
        public void TestDigitWordExtraction3A()
        {
            var cw = new CheckWordster("32768");
            Assert.Equal("thirty two", cw.WordsForUpTo1000("32"));
        }

        [Fact]
        public void TestDigitWordExtraction3B()
        {
            var cw = new CheckWordster("32768");
            Assert.Equal("seven hundred sixty eight", cw.WordsForUpTo1000("768"));
        }


        [Fact]
        public void TestWordForDigits3()
        {
            var cw = new CheckWordster("32768");
            Assert.Equal("Thirty two thousand seven hundred sixty eight", cw.GetWords());
        }

        [Fact]
        public void TestDigitWordExtraction4A()
        {
            var cw = new CheckWordster("32000");
            Assert.Equal("thirty two", cw.WordsForUpTo1000("32"));
        }

        [Fact]
        public void TestDigitWordExtraction4B()
        {
            var cw = new CheckWordster("32000");
            Assert.Equal("", cw.WordsForUpTo1000("000"));
        }

        [Fact]
        public void TestWordForDigits4()
        {
            var cw = new CheckWordster("32000");
            Assert.Equal("Thirty two thousand", cw.GetWords());
        }

        [Fact]
        public void TestDigitWordExtraction5A()
        {
            var cw = new CheckWordster("32001");
            Assert.Equal("thirty two", cw.WordsForUpTo1000("32"));
        }

        [Fact]
        public void TestDigitWordExtraction5B()
        {
            var cw = new CheckWordster("32001");
            Assert.Equal("one", cw.WordsForUpTo1000("001"));
        }

        [Fact]
        public void TestWordForDigits5()
        {
            var cw = new CheckWordster("32001");
            Assert.Equal("Thirty two thousand one", cw.GetWords());
        }

    }
}
