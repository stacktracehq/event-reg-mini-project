using System;

namespace Whiteboard.Registration.Domain
{
    public class TinyType<T>
    {
        public TinyType(T value)
        {
            Value = value;
        }
        public T Value { get; }
    }
}