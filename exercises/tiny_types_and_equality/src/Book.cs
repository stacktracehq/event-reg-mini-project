using System;


namespace Exercises
{
    public class TinyType<T> : IEquatable<TinyType<T>>, IComparable<TinyType<T>> where T : IComparable<T>
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