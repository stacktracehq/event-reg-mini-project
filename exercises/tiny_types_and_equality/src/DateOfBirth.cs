using System;

namespace Exercises
{
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
}
