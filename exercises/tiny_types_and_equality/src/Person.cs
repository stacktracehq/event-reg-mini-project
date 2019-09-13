`using System;

namespace Exercises
{
    public class Person
    {
        public string Name {get;}
        public DateTime DateOfBirth {get;}
        public Person(string name, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("A person must have a name");
            }

            if (dateOfBirth > DateTime.Now)
            {
                throw new ArgumentException("A person cannot have a date of birth in the future");
            }

            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public int GetAgeInYears()
        {
            return DateTime.Now.Year - DateOfBirth.Year;

        }

    }

}
// Reflective Questions
//  1. It is not possible for Datetime to be null. They would be set to `DateTime.MinValue`
//  2. Right now I am only getting age using the current year, so it might not be accurate depending on if the month has passed....