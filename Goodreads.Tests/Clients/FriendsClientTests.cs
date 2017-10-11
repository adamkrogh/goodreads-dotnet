using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class FriendsClientTests
    {
        private readonly IFriendsClient FriendsClient;

        public FriendsClientTests()
        {
            FriendsClient = Helper.GetAuthClient().Friends;
        }

        public class TheGetFriendRequestMethod : FriendsClientTests
        {
            [Fact]
            public async Task GetFriendRequestMethod()
            {
                var requests = await FriendsClient.GetFriendRequests();

                Assert.NotNull(requests);
            }
        }
    }
}
