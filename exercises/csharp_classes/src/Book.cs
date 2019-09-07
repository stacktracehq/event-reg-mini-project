using System;

namespace Exercises
{
    public class Book
    {
        public int CurrentPage { get; private set; }

        public Book(string title, string author, int numberOfPages)
        {
        }

        public void SetBookmark(int pageNumber)
        {
            throw new NotImplementedException(nameof(SetBookmark));
        }

        public void ClearBookmark()
        {
            throw new NotImplementedException(nameof(ClearBookmark));
        }

        public int EstimateRemainingReadingMinutes(double pagesReadPerMinute)
        {
            throw new NotImplementedException(nameof(EstimateRemainingReadingMinutes));
        }
    }

}
