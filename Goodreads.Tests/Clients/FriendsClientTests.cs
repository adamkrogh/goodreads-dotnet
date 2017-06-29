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
    }
}
