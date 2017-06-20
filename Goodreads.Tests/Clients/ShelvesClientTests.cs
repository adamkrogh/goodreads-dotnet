using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests.Clients
{
    public class ShelvesClientTests
    {
        private readonly IShelvesClient ShelvesClient;

        public ShelvesClientTests()
        {
            ShelvesClient = Helper.GetAuthClient().Shelves;
        }

        public class TheGetListOfUserShelvesMethod : ShelvesClientTests
        {
            [Fact]
            public async Task ReturnsShelves()
            {
                var userId = 7284465;
                var shelves = await ShelvesClient.GetListOfUserShelves(userId);

                Assert.NotNull(shelves);
                Assert.NotEmpty(shelves.List);
                Assert.True(shelves.Pagination.TotalItems > 0);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var userId = -1;
                var shelves = await ShelvesClient.GetListOfUserShelves(userId);

                Assert.Null(shelves);
            }
        }

        public class TheAddBookToShelfMethod : ShelvesClientTests
        {
            [Fact]
            public async Task AddBookToShelf()
            {
                var shelf = "to-read";
                var bookId = 7235533;
                var result = await ShelvesClient.AddBookToShelf(shelf, bookId);

                Assert.True(result);
            }

            [Fact]
            public async Task RemoveBookFromShelf()
            {
                var shelf = "to-read";
                var bookId = 7235533;
                await ShelvesClient.AddBookToShelf(shelf, bookId);

                var result = await ShelvesClient.AddBookToShelf(shelf, bookId, "remove");

                Assert.True(result);
            }

            [Fact]
            public async Task RemoveNotExistingBookFromShelf()
            {
                var shelf = "read";
                var bookId = 1;
                var result = await ShelvesClient.AddBookToShelf(shelf, bookId, "remove");

                Assert.False(result);
            }
        }

        public class TheAddBooksToShelvesMethod : ShelvesClientTests
        {
            [Fact]
            public async Task AddBooksToShelves()
            {
                var shelves = new[] { "to-read", "leo" };
                var bookIds = new[] { 15823480, 656 };
                var result = await ShelvesClient.AddBooksToShelves(shelves, bookIds);

                Assert.True(result);
            }
        }
    }
}
