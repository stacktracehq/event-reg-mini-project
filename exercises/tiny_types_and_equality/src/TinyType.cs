using System;

namespace Exercises
{
    public class TinyType<T> : IEquatable<TinyType<T>>, IComparable<TinyType<T>> where T : IComparable<T>
    {
        public TinyType(T value)
        {
            Value = value;
        }

        public T Value { get; }

        //  comparing
        public int CompareTo(TinyType<T> other) =>
            Value.CompareTo(other.Value);

        public static bool operator >= (TinyType<T> a, TinyType<T> b) =>
            a.CompareTo(b) >= 0;
        public static bool operator <= (TinyType<T> a, TinyType<T> b) =>
            a.CompareTo(b) <= 0;
        public static bool operator > (TinyType<T> a, TinyType<T> b) =>
            a.CompareTo(b) == 1;
        public static bool operator < (TinyType<T> a, TinyType<T> b) =>
            a.CompareTo(b) == -1;
        public static bool operator > (TinyType<T> a, int b) =>
            a is TinyType<int> t && t.Value.CompareTo(b) == 1;
        public static bool operator < (TinyType<T> a, int b) =>
            a is TinyType<int> t && t.Value.CompareTo(b) == -1;

        // equating
        public override bool Equals(object obj) =>
            obj is T t && Equals(t);

        public bool Equals(TinyType<T> other) =>
            other != null && Equals(Value, other);

        public override int GetHashCode() =>
            Value.GetHashCode();

        public static bool operator ==(TinyType<T> a, TinyType<T> b) =>
             Equals(a, b);

        public static bool operator !=(TinyType<T> a, TinyType<T> b) =>
            !Equals(a, b);
    }

}
