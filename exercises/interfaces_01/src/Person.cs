using System;

namespace Exercises
{
    public class Person
    {
        public Name Name {get;}
        public DateOfBirth DateOfBirth {get;}

        public Person(Name name, DateOfBirth dateOfBirth)
        {
            Name = new Name(name.Value);
            DateOfBirth = new DateOfBirth(dateOfBirth.Value);
        }

        public int GetAgeInYears()
        {
            var now = DateTime.Now;
            var alreadyPassed =
                DateOfBirth.Value.Month <= now.Month
                && DateOfBirth.Value.Day <= now.Day;

            return alreadyPassed
                ? DateTime.Now.Year - DateOfBirth.Value.Year
                : DateTime.Now.Year - DateOfBirth.Value.Year - 1;
        }

    }

}
