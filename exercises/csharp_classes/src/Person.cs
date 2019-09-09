using System;

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
