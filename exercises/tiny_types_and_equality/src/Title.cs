using System;

namespace Exercises
{
    public class Title : TinyType<string>
    {
        public Title(string value) : base(value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("A book must have a title");
            }
        }
    }
}
