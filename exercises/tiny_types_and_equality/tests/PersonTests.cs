using Xunit;
using Exercises;
using System;

namespace Tests
{
    public class PersonTests
    {
        private readonly DateTime _bradsBirthday = new DateTime(1987, 3, 6);
        private readonly DateTime _colleensBirthday = new DateTime(1977, 3, 5);

        [Fact]
        public void AgeIsCalculatedCorrectlyForDatesInThePast()
        {
            var brad = new Person(new Name("Brad"), new DateOfBirth( new DateTime(1987, 3, 6) ));
            Assert.Equal(32, brad.GetAgeInYears());

            var colleen = new Person(new Name("Colleen"), new DateOfBirth(new DateTime(1977, 3, 6)));
            Assert.Equal(42, colleen.GetAgeInYears());
        }
    }

}
