using System.Threading.Tasks;
using Goodreads.Clients;
using Goodreads.Models.Request;
using Xunit;

namespace Goodreads.Tests
{
    public class UpdatesEndpointTests
    {
        private readonly IOAuthUpdatesEndpoint UpdatesEndpoint;

        public UpdatesEndpointTests()
        {
            UpdatesEndpoint = Helper.GetAuthClient().Updates;
        }

        public class TheGetFriensUpdateMethod : UpdatesEndpointTests
        {
            [Fact]
            public async Task GetFriendsUpdates()
            {
                var updates = await UpdatesEndpoint.GetFriendsUpdates(UpdateType.Reviews);
                Assert.NotNull(updates);
            }
        }
    }
}
