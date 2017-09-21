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
    }
}
