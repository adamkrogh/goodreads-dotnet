using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class FollowersEndpointTests
    {
        private readonly IOAuthFollowersEndpoint FollowersEndpoint;
        private readonly long UserId;

        public FollowersEndpointTests()
        {
            FollowersEndpoint = Helper.GetAuthClient().Followers;
            UserId = Helper.GetUserId();
        }

        public class TheFollowMethod : FollowersEndpointTests
        {
            [Fact]
            public async Task FollowUser()
            {
                var userId = 690273;
                var userFollowing = await FollowersEndpoint.Follow(userId);

                await FollowersEndpoint.Unfollow(userId); // cleanup following

                Assert.NotNull(userFollowing);
                Assert.Equal(userFollowing.UserId, userId);
                Assert.Equal(userFollowing.FollowerId, UserId);
            }
        }

        public class TheUnfollowMethod : FollowersEndpointTests
        {
            [Fact]
            public async Task UnfollowUser()
            {
                var userId = 700809;
                await FollowersEndpoint.Follow(userId); // arrange following

                var result = await FollowersEndpoint.Unfollow(userId); // cleanup following

                Assert.True(result);
            }
        }
    }
}
