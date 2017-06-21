using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests.Clients
{
    public class AuthorsFollowingClientTests
    {
        private readonly IAuthorsFollowingClient AuthorsFollowingClient;

        public AuthorsFollowingClientTests()
        {
            AuthorsFollowingClient = Helper.GetAuthClient().AuthorsFollowing;
        }

        public class TheFollowMethod : AuthorsFollowingClientTests
        {
            [Fact]
            public async Task FollowAnAuthor()
            {
                var authorId = 38550;
                var authorFollowing = await AuthorsFollowingClient.Follow(authorId);

                await AuthorsFollowingClient.Unfollow(authorFollowing.Id); // cleanup following

                Assert.NotNull(authorFollowing);
                Assert.NotSame(authorFollowing.Id, default(int));
            }
        }

        public class TheUnfollowMethod : AuthorsFollowingClientTests
        {
            [Fact]
            public async Task UnfollowAnAuthor()
            {
                var authorId = 38551;
                var authorFollowing = await AuthorsFollowingClient.Follow(authorId); // arrange following

                var result = await AuthorsFollowingClient.Unfollow(authorFollowing.Id);

                Assert.True(result);
            }
        }
    }
}
