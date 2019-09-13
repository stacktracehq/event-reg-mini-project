using System;

namespace Exercises
{
    // public class TinyType<T>
    // {
    //     public TinyType(T value)
    //     {
    //         Value = value;
    //     }
    //     public T Value { get; }
    // }

    public class Name : TinyType<string>
    {
        public Name(string value) : base(value)
        {
            if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("A person must have a name");
                }
        }
    }

    public class DateOfBirth : TinyType<DateTime>
    {
        public DateOfBirth(DateTime value) : base(value)
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("A person cannot have a date of birth in the future");
            }
        }
    }

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
            return DateTime.Now.Year - DateOfBirth.Value.Year;

        }

    }

}
// Reflective Questions
//  1. It is not possible for Datetime to be null. They would be set to `DateTime.MinValue`
//  2. Right now I am only getting age using the current year, so it might not be accurate depending on if the month has passed....