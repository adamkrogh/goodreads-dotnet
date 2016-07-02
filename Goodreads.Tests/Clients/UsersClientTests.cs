using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests.Clients
{
    public class UsersClientTests
    {
        private readonly IUsersClient UsersClient;
        private readonly int UserId;

        public UsersClientTests()
        {
            UsersClient = Helper.GetAuthClient().Users;
            UserId = Helper.GetUserId();
        }

        public class TheGetListOfFriendsMethod : UsersClientTests
        {
            [Fact]
            public async Task ReturnsFriends()
            {
                var friends = await UsersClient.GetListOfFriends(UserId);

                Assert.NotNull(friends);
                Assert.NotEmpty(friends.List);
                Assert.True(friends.Pagination.TotalItems > 0);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var friends = await UsersClient.GetListOfFriends(userId: -1);

                Assert.Null(friends);
            }
        }
    }
}
