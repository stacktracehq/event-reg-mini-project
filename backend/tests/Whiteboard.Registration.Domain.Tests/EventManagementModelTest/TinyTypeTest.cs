using System;
using Xunit;
using Whiteboard.Registration.Domain;

namespace Whiteboard.Registration.Domain.Tests
{
    public class TinyTypeTest
    {
        [Fact]
        public void TinyTypeCompareToOtherValueTest()
        {
            // For this the TinyTypes must be an int?
            var testTinyType = new TinyType<int>(3);
            var testTinyType2 = new TinyType<int>(3);
            var compareToTest = testTinyType.CompareTo(testTinyType2);
            Assert.Equal(0, compareToTest);

            var testTinyType3 = new TinyType<int>(2);
            Assert.Equal(1, testTinyType.CompareTo(testTinyType3));
        }

        [Fact]
        public void TinyTypeGreaterThanEqualToTest()
        {
            // ints
            var testTinyType = new TinyType<int>(3);
            var testTinyType2 = new TinyType<int>(2);
            Assert.True(testTinyType >= testTinyType2);

            // string are equal
            var testString = new TinyType<string>("hello");
            var testString2 = new TinyType<string>("hello");
            Assert.True(testString >= testString2);

            // string, not equal
            var testString3 = new TinyType<string>("he");
            Assert.True(testString >= testString3);
            Assert.False(testString3 >= testString2);
        }

        [Fact]
        public void TinyTypeGreaterThanTest()
        {}
    }
}
