using System;
using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class UsersEndpointTests
    {
        private readonly IOAuthUsersEndpoint UsersEndpoint;
        private readonly long UserId;

        public UsersEndpointTests()
        {
            UsersEndpoint = Helper.GetAuthClient().Users;
            UserId = Helper.GetUserId();
        }

        public class TheGetByUserIdMethod : UsersEndpointTests
        {
            [Fact]
            public async Task ReturnsAUser()
            {
                var user = await UsersEndpoint.GetByUserId(UserId);

                Assert.NotNull(user);
                Assert.Equal(user.Id, UserId);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var user = await UsersEndpoint.GetByUserId(long.MaxValue);

                Assert.Null(user);
            }
        }

        public class TheGetByUsernameMethod : UsersEndpointTests
        {
            [Fact]
            public async Task ReturnsAUser()
            {
                var username = "adamkrogh";
                var user = await UsersEndpoint.GetByUsername(username);

                Assert.NotNull(user);
                Assert.Equal(user.Username, username);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var username = Guid.NewGuid().ToString().Replace("-", string.Empty);
                var user = await UsersEndpoint.GetByUsername(username);

                Assert.Null(user);
            }
        }

        public class TheGetListOfFriendsMethod : UsersEndpointTests
        {
            [Fact]
            public async Task ReturnsFriends()
            {
                var friends = await UsersEndpoint.GetListOfFriends(UserId);

                Assert.NotNull(friends);
                Assert.NotEmpty(friends.List);
                Assert.True(friends.Pagination.TotalItems > 0);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var friends = await UsersEndpoint.GetListOfFriends(userId: -1);

                Assert.Null(friends);
            }
        }

        public class TheGetAuthenticatedUserIdMethod : UsersEndpointTests
        {
            [Fact]
            public async Task ReturnsUserIdWhenAuthenticated()
            {
                var id = await UsersEndpoint.GetAuthenticatedUserId();
                Assert.Equal(id, Helper.GetUserId());
            }
        }

        public class TheGetUserFollowingMethod : UsersEndpointTests
        {
            [Fact]
            public async Task ReturnUserFollowing()
            {
                const int userId = 68628513;
                var followings = await UsersEndpoint.GetUserFollowing(userId);

                Assert.NotNull(followings);
                Assert.NotEmpty(followings.List);
            }
        }

        public class TheGetUsersFollowersMethod : UsersEndpointTests
        {
            [Fact]
            public async Task ReturnUsersFollowers()
            {
                const int userId = 68628513;
                var followings = await UsersEndpoint.GetUsersFollowers(userId);

                Assert.NotNull(followings);
                Assert.NotEmpty(followings.List);
            }
        }
    }
}
