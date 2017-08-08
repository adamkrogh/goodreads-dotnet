using Goodreads.Clients;
using Goodreads.Models.Request;
using System.Threading.Tasks;
using Xunit;

namespace Goodreads.Tests
{
    public class UpdatesClientTests
    {
        private readonly IUpdatesClient UpdatesClient;

        public UpdatesClientTests()
        {
            UpdatesClient = Helper.GetAuthClient().Updates;
        }

        public class TheGetFriensUpdateMethod : UpdatesClientTests
        {
            [Fact]
            public async Task GetFriendsUpdates()
            {
                var updates = await UpdatesClient.GetFriendsUpdates(UpdateType.Reviews);
                Assert.NotNull(updates);
            }
        }
    }
}
