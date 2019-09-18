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

        private static readonly MockTimeProvider _now =
            new MockTimeProvider(_today);

        [Fact]
        public void WhenBirthdayWasYesterday()
        {
            var person = new Person(
                new Name("Bob"),
                new DateOfBirth(_yesterday.AddYears(-32))
            );
            // _now = new MockTimeProvider(_yesterday);
            Assert.Equal(32, person.GetAgeInYears(_now));
        }

        [Fact]
        public void WhenBirthdayIsToday()
        {
            var person = new Person(
                new Name("Bob"),
                new DateOfBirth(_today.AddYears(-32))
            );
            Assert.Equal(32, person.GetAgeInYears(_now));
        }

        [Fact]
        public void WhenBirthdayIsTomorrow()
        {
            var person = new Person(
                new Name("Bob"),
                new DateOfBirth(_tomorrow.AddYears(-32))
            );
            Assert.Equal(31, person.GetAgeInYears(_now));
        }
    }

}
