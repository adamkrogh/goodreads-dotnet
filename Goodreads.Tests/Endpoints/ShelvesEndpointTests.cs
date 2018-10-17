using System.Threading.Tasks;
using Goodreads.Clients;
using Goodreads.Exceptions;
using Xunit;

namespace Goodreads.Tests
{
    public class ShelvesEndpointTests
    {
        private readonly IOAuthShelvesEndpoint ShelvesEndpoint;

        public ShelvesEndpointTests()
        {
            ShelvesEndpoint = Helper.GetAuthClient().Shelves;
        }

        public class TheGetListOfUserShelvesMethod : ShelvesEndpointTests
        {
            [Fact]
            public async Task ReturnsShelves()
            {
                var userId = 7284465;
                var shelves = await ShelvesEndpoint.GetListOfUserShelves(userId);

                Assert.NotNull(shelves);
                Assert.NotEmpty(shelves.List);
                Assert.True(shelves.Pagination.TotalItems > 0);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var userId = -1;
                var shelves = await ShelvesEndpoint.GetListOfUserShelves(userId);

                Assert.Null(shelves);
            }
        }

        public class TheAddBookToShelfMethod : ShelvesEndpointTests
        {
            [Fact]
            public async Task AddBookToShelf()
            {
                var shelf = "to-read";
                var bookId = 7235533;
                var result = await ShelvesEndpoint.AddBookToShelf(shelf, bookId);

                Assert.True(result);
            }

            [Fact]
            public async Task RemoveBookFromShelf()
            {
                var shelf = "to-read";
                var bookId = 7235533;
                await ShelvesEndpoint.AddBookToShelf(shelf, bookId);

                var result = await ShelvesEndpoint.AddBookToShelf(shelf, bookId, "remove");

                Assert.True(result);
            }

            [Fact]
            public async Task RemoveNotExistingBookFromShelf()
            {
                var shelf = "read";
                var bookId = 1;
                var result = await ShelvesEndpoint.AddBookToShelf(shelf, bookId, "remove");

                Assert.False(result);
            }
        }

        public class TheAddBooksToShelvesMethod : ShelvesEndpointTests
        {
            [Fact]
            public async Task AddBooksToShelves()
            {
                var shelves = new[] { "to-read", "leo" };
                var bookIds = new long[] { 15823480, 656 };
                var result = await ShelvesEndpoint.AddBooksToShelves(shelves, bookIds);

                Assert.True(result);
            }
        }

        public class TheAddUserShelfMethod : ShelvesEndpointTests
        {
            [Fact]
            public async void AddExistingShelf()
            {
                const string name = "to-read";
                await Assert.ThrowsAsync<ApiException>(() => ShelvesEndpoint.AddShelf(name));
            }
        }
    }
}
