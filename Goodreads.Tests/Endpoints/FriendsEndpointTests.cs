using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class FriendsEndpointTests
    {
        private readonly IOAuthFriendsEndpoint FriendsEndpoint;

        public FriendsEndpointTests()
        {
            FriendsEndpoint = Helper.GetAuthClient().Friends;
        }

        public class TheGetFriendRequestMethod : FriendsEndpointTests
        {
            [Fact]
            public async Task GetFriendRequestMethod()
            {
                var requests = await FriendsEndpoint.GetFriendRequests();

                Assert.NotNull(requests);
            }
        }
    }
}
