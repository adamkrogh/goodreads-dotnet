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
            ShelvesClient = Helper.GetClient().Shelves;
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
    }
}
