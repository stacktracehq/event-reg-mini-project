using System;

namespace Exercises
{
    public class Book
    {
        public int CurrentPage { get; private set; }
        public string Title { get; }
        public string Author { get; }
        public int NumberOfPages { get; }

        public Book(string title, string author, int numberOfPages)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("A book must have a title");
            }

            if (string.IsNullOrEmpty(author))
            {
                throw new ArgumentException("A book must have an author");
            }

            if (numberOfPages <= 0)
            {
                throw new ArgumentException("A book must have at least one page");
            }

            Title = title;
            Author = author;
            NumberOfPages = numberOfPages;
            CurrentPage = 1;
        }

        public void SetBookmark(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > NumberOfPages)
            {
                throw new ArgumentException("Bookmarked page must be in range 1-" + NumberOfPages);
            }
            CurrentPage = pageNumber;
        }

        public void ClearBookmark()
        {
            CurrentPage = 1;
        }

        public int EstimateRemainingReadingMinutes(double pagesReadPerMinute)
        {
            int pagesLeftToRead = NumberOfPages - CurrentPage + 1;
            if (pagesLeftToRead == 1 && pagesReadPerMinute > 1)
            {
                return 1;
            }
            double estimatedRemainingMinutes = pagesLeftToRead / pagesReadPerMinute;
            return Convert.ToInt32(estimatedRemainingMinutes);
        }
    }

}

// Reflective questions
//  1. Use `unit` as the type
//  2.  https://mentormate.com/bg/blog/modern-validation-patterns-in-c-sharp/
//  3. using named parameters/arguments - new Book(author: "Stephen King", title: "Pet Cemetary", 230)
//  4. Perhaps they would not be equal - different memory locations?
