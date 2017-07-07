using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class GroupClientTests
    {
        private readonly IGroupClient GroupClient;

        public GroupClientTests()
        {
            GroupClient = Helper.GetAuthClient().Groups;
        }

        public class TheJoinMethod : GroupClientTests
        {
            [Fact(Skip = "Impossible to test because I can't leave group using the Goodreads API. So I can't clean up a test suite.")]
            public void JoinToGroup()
            {
            }
        }
    }
}
