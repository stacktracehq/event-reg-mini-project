using System;

namespace Whiteboard.Registration.Domain.Tiny
{
    public abstract class TinyType<T> :
        IEquatable<TinyType<T>>,
        IEquatable<T>
            where T :  IEquatable<T>
    {
        protected TinyType(T value)
        {
            Value = value;
        }

        public T Value { get; }

        // equating
        public override bool Equals(object obj) =>
            obj is TinyType<T> tt && Equals(tt) || obj is T t && Equals(t);

        public bool Equals(TinyType<T> other) =>
            other != null && Value.Equals(other.Value);

        public bool Equals(T other) =>
            other != null && Value.Equals(other);

        public override int GetHashCode() =>
            Value.GetHashCode();

        public static bool operator ==(TinyType<T> a, TinyType<T> b)
        {
            if (a is null && b is null) return true;
            return !(a is null) && a.Equals(b);
        }

        public static bool operator ==(TinyType<T> a, T b) =>
            a?.Value.Equals(b) ?? false;

        public static bool operator !=(TinyType<T> a, T b) =>
            !(a == b);

        public static bool operator !=(TinyType<T> a, TinyType<T> b) =>
            !(a == b);
    }

}
