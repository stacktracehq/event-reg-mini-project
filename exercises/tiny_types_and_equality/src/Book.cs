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
    public class Book
    {
        public PageNumber CurrentPage { get; private set; }
        public Title Title { get; }
        public Author Author { get; }
        public PageNumber NumberOfPages { get; }

        public Book(Title title, Author author, PageNumber numberOfPages)
        {


            Title = title;
            Author = author;
            NumberOfPages = numberOfPages;
            CurrentPage = new PageNumber(1);
        }

        public void SetBookmark(PageNumber pageNumber)
        {
            if (pageNumber < 1 || pageNumber.Value > NumberOfPages.Value)
            {
                throw new ArgumentException("Bookmarked page must be in range 1-" + NumberOfPages.Value);
            }
            CurrentPage = pageNumber;
        }

        public void ClearBookmark()
        {
            CurrentPage = new PageNumber(1);
        }

        public int EstimateRemainingReadingMinutes(double pagesReadPerMinute)
        {
            int pagesLeftToRead = NumberOfPages.Value - CurrentPage.Value + 1;
            if (pagesLeftToRead == 1 && pagesReadPerMinute > 1)
            {
                return 1;
            }
            double estimatedRemainingMinutes = pagesLeftToRead / pagesReadPerMinute;
            return Convert.ToInt32(estimatedRemainingMinutes);
        }
    }

}