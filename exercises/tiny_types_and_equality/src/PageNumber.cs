using System;

namespace Exercises
{
    public class PageNumber : TinyType<int>
    {
        public PageNumber(int value) : base(value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("A book must have at least one page");
            }
        }
    }
}
