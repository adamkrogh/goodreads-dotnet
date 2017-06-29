using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class FollowersClientTests
    {
        private readonly IFollowersClient FollowersClient;
        private readonly int UserId;

        public FollowersClientTests()
        {
            FollowersClient = Helper.GetAuthClient().Followers;
            UserId = Helper.GetUserId();
        }

        public class TheFollowMethod : FollowersClientTests
        {
            [Fact]
            public async Task FollowUser()
            {
                var userId = 690273;
                var userFollowing = await FollowersClient.Follow(userId);

                await FollowersClient.Unfollow(userId); // cleanup following

                Assert.NotNull(userFollowing);
                Assert.Equal(userFollowing.UserId, userId);
                Assert.Equal(userFollowing.FollowerId, UserId);
            }
        }

        public class TheUnfollowMethod : FollowersClientTests
        {
            [Fact]
            public async Task UnfollowUser()
            {
                var userId = 700809;
                await FollowersClient.Follow(userId); // arrange following

                var result = await FollowersClient.Unfollow(userId); // cleanup following

                Assert.True(result);
            }
        }
    }
}
