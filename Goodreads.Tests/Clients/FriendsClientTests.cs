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

        public class TheAddFriendMethod : FriendsClientTests
        {
            [Fact(Skip = "Impossible to test because I can't remove friend using the Goodreads API. So I can't clean up a test suite.")]
            public void AddFriendMethod()
            {
            }
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

        public class TheConfirmFriendRequestMethod : FriendsClientTests
        {
            [Fact(Skip = "Impossible to test because I can't create friend request from another user.")]
            public void ConfirmFriendRequestMethod()
            {
            }
        }
    }
}
