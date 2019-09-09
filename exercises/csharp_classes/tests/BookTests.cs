using Xunit;
using Exercises;
using System;

namespace Tests
{
    public class BookTests
    {
        [Fact]
        public void CannotConstructABookWithEmptyOrNullTitle()
        {
            var exceptionFromEmpty = Assert.Throws<ArgumentException>(() => {
                new Book("", "Stephen King", 230);
            });

            Assert.Contains(
                "A book must have a title",
                exceptionFromEmpty.Message
            );

            var exceptionFromNull = Assert.Throws<ArgumentException>(() => {
                new Book(null, "Stephen King", 230);
            });

            Assert.Contains(
                "A book must have a title",
                exceptionFromNull.Message
            );
        }

        [Fact]
        public void CannotConstructABookWithEmptyOrNullAuthor()
        {
            var exceptionFromEmpty = Assert.Throws<ArgumentException>(() => {
                new Book("Pet Cemetary", "", 230);
            });

            Assert.Contains(
                "A book must have an author",
                exceptionFromEmpty.Message
            );

            var exceptionFromNull = Assert.Throws<ArgumentException>(() => {
                new Book("Pet Cemetary", null, 230);
            });

            Assert.Contains(
                "A book must have an author",
                exceptionFromNull.Message
            );
        }

        [Fact]
        public void CannotConstructABookWithANegativeNumberOfPages()
        {
            var exception = Assert.Throws<ArgumentException>(() => {
                new Book("Pet Cemetary", "Stephen King", -1);
            });

            Assert.Contains(
                "A book must have at least one page",
                exception.Message
            );
        }

        [Fact]
        public void CannotConstructABookWithZeroPages()
        {
            var exception = Assert.Throws<ArgumentException>(() => {
                new Book("Pet Cemetary", "Stephen King", 0);
            });

            Assert.Contains(
                "A book must have at least one page",
                exception.Message
            );
        }

        [Fact]
        public void SettingABookmarkUpdatesCurrentPage()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);
            petCemetary.SetBookmark(100);
            Assert.Equal(100, petCemetary.CurrentPage);
        }

        [Fact]
        public void SettingABookmarkToANegativePageNumberThrows()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);

            var exception = Assert.Throws<ArgumentException>(() => {
                petCemetary.SetBookmark(-1);
            });

            Assert.Contains(
                "Bookmarked page must be in range 1-230",
                exception.Message
            );
        }

        [Fact]
        public void SettingABookmarkToPageZeroThrows()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);

            var exception = Assert.Throws<ArgumentException>(() => {
                petCemetary.SetBookmark(0);
            });

            Assert.Contains(
                "Bookmarked page must be in range 1-230",
                exception.Message
            );
        }

        [Fact]
        public void SettingABookmarkToAPageGreaterThanNumberOfPagesThrows()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);

            var exception = Assert.Throws<ArgumentException>(() => {
                petCemetary.SetBookmark(231);
            });

            Assert.Contains(
                "Bookmarked page must be in range 1-230",
                exception.Message
            );
        }

        [Fact]
        public void CanClearABookmarkEvenWhenOneIsNotSet()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);
            petCemetary.ClearBookmark();
        }

        [Fact]
        public void ClearingASetBookMarkSetsTheCurrentPageBackToOne()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);
            petCemetary.SetBookmark(45);
            petCemetary.ClearBookmark();
            Assert.Equal(1, petCemetary.CurrentPage);
        }

        [Fact]
        public void MultipleCallsToSetBookmarkUpdateTheCurrentPage()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);
            petCemetary.SetBookmark(45);
            Assert.Equal(45, petCemetary.CurrentPage);
            petCemetary.SetBookmark(78);
            Assert.Equal(78, petCemetary.CurrentPage);
        }

        [Fact]
        public void WhenBookNotStartedThenEstimatedRemainingMinutesIsCorrect()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);
            const double readingRate = 2.3;

            var remainingMinutes = petCemetary
                .EstimateRemainingReadingMinutes(readingRate);

            Assert.Equal(100, remainingMinutes);
        }

        [Fact]
        public void WhenPartiallyCompletedEstimatedRemainingMinutesIsCorrect()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);
            petCemetary.SetBookmark(45);
            const double readingRate = 2.3;

            var remainingMinutes = petCemetary
                .EstimateRemainingReadingMinutes(readingRate);

            Assert.Equal(81, remainingMinutes);
        }

        [Fact]
        public void WhenOnLastPageAndReadingRateGreaterThanAPageAMinuteThenOneMinuteLeft()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);
            petCemetary.SetBookmark(230);
            const double readingRate = 2.3;

            var remainingMinutes = petCemetary
                .EstimateRemainingReadingMinutes(readingRate);

            Assert.Equal(1, remainingMinutes);
        }

        [Fact]
        public void WhenOnLastPageAndReadingRateLessThanAPageAMinuteThenEstimateCorrect()
        {
            var petCemetary = new Book("Pet Cemetary", "Stephen King", 230);
            petCemetary.SetBookmark(230);
            const double readingRate = 0.25;

            var remainingMinutes = petCemetary
                .EstimateRemainingReadingMinutes(readingRate);

            Assert.Equal(4, remainingMinutes);
        }
    }

}
