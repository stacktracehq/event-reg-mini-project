using System;

namespace Exercises
{
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
}
