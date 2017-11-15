using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class AuthorsFollowingEndpointTests
    {
        private readonly IOAuthAuthorsFollowingEndpoint AuthorsFollowingEndpoint;

        public AuthorsFollowingEndpointTests()
        {
            AuthorsFollowingEndpoint = Helper.GetAuthClient().AuthorsFollowing;
        }

        public class TheFollowMethod : AuthorsFollowingEndpointTests
        {
            [Fact]
            public async Task FollowAnAuthor()
            {
                var authorId = 3173264;
                var authorFollowing = await AuthorsFollowingEndpoint.Follow(authorId);

                await AuthorsFollowingEndpoint.Unfollow(authorFollowing.Id); // cleanup following

                Assert.NotNull(authorFollowing);
                Assert.NotEqual(default(long), authorFollowing.Id);
            }
        }

        public class TheUnfollowMethod : AuthorsFollowingEndpointTests
        {
            [Fact]
            public async Task UnfollowAnAuthor()
            {
                var authorId = 1077326;
                var authorFollowing = await AuthorsFollowingEndpoint.Follow(authorId); // arrange following

                var result = await AuthorsFollowingEndpoint.Unfollow(authorFollowing.Id); // cleanup following

                Assert.True(result);
            }
        }

        public class TheShowMethod : AuthorsFollowingEndpointTests
        {
            [Fact]
            public async Task Show()
            {
                var authorId = 4470846;
                var followInfo = await AuthorsFollowingEndpoint.Follow(authorId); // arrange following

                var responseInfo = await AuthorsFollowingEndpoint.Show(followInfo.Id);

                await AuthorsFollowingEndpoint.Unfollow(followInfo.Id); // cleanup following

                Assert.NotNull(responseInfo);
                Assert.Equal(followInfo.Id, responseInfo.Id);
                Assert.Equal(followInfo.Author.Id, responseInfo.Author.Id);
                Assert.Equal(followInfo.User.Id, responseInfo.User.Id);
            }

            [Fact]
            public async Task ShowNotExistingInfo()
            {
                var responseInfo = await AuthorsFollowingEndpoint.Show(51523178);
                Assert.Null(responseInfo);
            }
        }
    }
}
