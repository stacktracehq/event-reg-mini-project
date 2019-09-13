using Xunit;
using Exercises;
using System;

namespace Tests
{
    public class PersonTests
    {
        private readonly DateTime _bradsBirthday = new DateTime(1987, 3, 6);
        private readonly DateTime _colleensBirthday = new DateTime(1977, 3, 6);

        [Fact]
        public void CannotConstructAPersonWithNullName()
        {
            var exception = Assert.Throws<ArgumentException>(() => {
                new Person(new Name(null), new DateOfBirth(_bradsBirthday));
            });

            Assert.Contains(
                "A person must have a name",
                exception.Message
            );
        }

        [Fact]
        public void CannotConstructAPersonWithAnEmptyName()
        {
            var exception = Assert.Throws<ArgumentException>(() => {
                new Person(new Name(string.Empty), new DateOfBirth( _bradsBirthday ));
            });

            Assert.Contains(
                "A person must have a name",
                exception.Message
            );
        }


        [Fact]
        public void CannotConstructAPersonWithADateOfBirthInTheFuture()
        {
            var exception = Assert.Throws<ArgumentException>(() => {
                new Person(new Name("Brad"), new DateOfBirth( DateTime.Now.AddYears(1) ));
            });

            Assert.Contains(
                "A person cannot have a date of birth in the future",
                exception.Message
            );
        }

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
