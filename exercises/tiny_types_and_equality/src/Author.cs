using System;

namespace Exercises
{
    public class Author : TinyType<string>
    {
        public Author(string value) : base(value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("A book must have an author");
            }
        }
    }
}
