using Xunit;
using Exercises;
using System;

namespace Tests
{
    public class PersonTests
    {
        private static readonly DateTime _today =
            new DateTime(2019, 9, 16);
        private static readonly DateTime _yesterday =
            new DateTime(2019, 9, 15);
        private static readonly DateTime _tomorrow =
            new DateTime(2019, 9, 17);

        [Fact]
        public void WhenBirthdayWasYesterday()
        {
            var person = new Person(
                new Name("Bob"),
                new DateOfBirth(_yesterday.AddYears(-32))
            );
            Assert.Equal(32, person.GetAgeInYears());
        }

        [Fact]
        public void WhenBirthdayIsToday()
        {
            var person = new Person(
                new Name("Bob"),
                new DateOfBirth(_today.AddYears(-32))
            );
            Assert.Equal(32, person.GetAgeInYears());
        }

        [Fact]
        public void WhenBirthdayIsTomorrow()
        {
            var person = new Person(
                new Name("Bob"),
                new DateOfBirth(_tomorrow.AddYears(-32))
            );
            Assert.Equal(31, person.GetAgeInYears());
        }
    }

}
