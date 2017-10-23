using System;
using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class OwnedBooksClientTests
    {
        private readonly IOwnedBookClient OwnedBookClient;

        public OwnedBooksClientTests()
        {
            OwnedBookClient = Helper.GetAuthClient().OwnedBooks;
        }

        public class TheGetAllOwnedBooksForUser : OwnedBooksClientTests
        {
            [Fact]
            public async Task TheGetAllOwnedBooks()
            {
                const int userId = 68628513;
                var books = await OwnedBookClient.GetOwnedBooks(userId);

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
            }
        }

        public class TheGetOwnedBooksInfo : OwnedBooksClientTests
        {
            [Fact]
            public async Task TheGetOwnedBookInfo()
            {
                const int ownedBookId = 48510472;
                var book = await OwnedBookClient.GetOwnedBookInfo(ownedBookId);

                Assert.NotNull(book);
                Assert.Equal(ownedBookId, book.Id);
            }
        }

        public class TheAddOwnedBooks : OwnedBooksClientTests
        {
            [Fact]
            public async Task TheAddOwnedBook()
            {
                const int bookId = 28646148;
                const int code = 20;
                const string description = @"Test description";
                var date = DateTime.UtcNow;
                const string location = @"Springfield";

                var ownedBook = await OwnedBookClient.AddOwnedBook(bookId, code, description, date, location);

                Assert.NotNull(ownedBook);
                Assert.Equal(bookId, ownedBook.BookId);
                Assert.Equal(code, ownedBook.ConditionCode);
                Assert.Equal(description, ownedBook.ConditionDescription);
                Assert.Equal(date.Date, ownedBook.OriginalPurchaseDate.Value.Date);
                Assert.Equal(location, ownedBook.OriginalPurchaseLocation);

                // remove just added book.
                await OwnedBookClient.DeleteOwnedBook(ownedBook.Id);
            }
        }

        public class TheDeleteOwnedBooks : OwnedBooksClientTests
        {
            [Fact]
            public async Task TheDeleteOwnedBook()
            {
                // add book for testing purpose.
                const long bookId = 46091;
                var ownedBook = await OwnedBookClient.AddOwnedBook(bookId);

                // test
                var result = await OwnedBookClient.DeleteOwnedBook(ownedBook.Id);

                Assert.True(result);
            }
        }
    }
}
