using System;
using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class OwnedBooksEndpointTests
    {
        private readonly IOAuthOwnedBooksEndpoint OwnedBookEndpoint;

        public OwnedBooksEndpointTests()
        {
            OwnedBookEndpoint = Helper.GetAuthClient().OwnedBooks;
        }

        public class TheGetAllOwnedBooksForUser : OwnedBooksEndpointTests
        {
            [Fact]
            public async Task TheGetAllOwnedBooks()
            {
                const int userId = 68628513;
                var books = await OwnedBookEndpoint.GetOwnedBooks(userId);

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
            }
        }

        public class TheGetOwnedBooksInfo : OwnedBooksEndpointTests
        {
            [Fact]
            public async Task TheGetOwnedBookInfo()
            {
                const int ownedBookId = 48510472;
                var book = await OwnedBookEndpoint.GetOwnedBookInfo(ownedBookId);

                Assert.NotNull(book);
                Assert.Equal(ownedBookId, book.Id);
            }
        }

        public class TheAddOwnedBooks : OwnedBooksEndpointTests
        {
            [Fact]
            public async Task TheAddOwnedBook()
            {
                const int bookId = 28646148;
                const int code = 20;
                const string description = @"Test description";
                var date = DateTime.UtcNow;
                const string location = @"Springfield";

                var ownedBook = await OwnedBookEndpoint.AddOwnedBook(bookId, code, description, date, location);

                Assert.NotNull(ownedBook);
                Assert.Equal(bookId, ownedBook.BookId);
                Assert.Equal(code, ownedBook.ConditionCode);
                Assert.Equal(description, ownedBook.ConditionDescription);
                Assert.Equal(date.Date, ownedBook.OriginalPurchaseDate.Value.Date);
                Assert.Equal(location, ownedBook.OriginalPurchaseLocation);

                // remove just added book.
                await OwnedBookEndpoint.DeleteOwnedBook(ownedBook.Id);
            }
        }

        public class TheDeleteOwnedBooks : OwnedBooksEndpointTests
        {
            [Fact]
            public async Task TheDeleteOwnedBook()
            {
                // add book for testing purpose.
                const long bookId = 46091;
                var ownedBook = await OwnedBookEndpoint.AddOwnedBook(bookId);

                // test
                var result = await OwnedBookEndpoint.DeleteOwnedBook(ownedBook.Id);

                Assert.True(result);
            }
        }
    }
}
