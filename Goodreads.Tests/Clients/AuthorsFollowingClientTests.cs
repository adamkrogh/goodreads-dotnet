using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class AuthorsFollowingClientTests
    {
        private readonly IOAuthAuthorsFollowingEndpoint AuthorsFollowingClient;

        public AuthorsFollowingClientTests()
        {
            AuthorsFollowingClient = Helper.GetAuthClient().AuthorsFollowing;
        }

        public class TheFollowMethod : AuthorsFollowingClientTests
        {
            [Fact]
            public async Task FollowAnAuthor()
            {
                var authorId = 3173264;
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
                var authorId = 1077326;
                var authorFollowing = await AuthorsFollowingClient.Follow(authorId); // arrange following

                var result = await AuthorsFollowingClient.Unfollow(authorFollowing.Id); // cleanup following

                Assert.True(result);
            }
        }

        public class TheShowMethod : AuthorsFollowingClientTests
        {
            [Fact]
            public async Task Show()
            {
                var authorId = 4470846;
                var followInfo = await AuthorsFollowingClient.Follow(authorId); // arrange following

                var responseInfo = await AuthorsFollowingClient.Show(followInfo.Id);

                await AuthorsFollowingClient.Unfollow(followInfo.Id); // cleanup following

                Assert.NotNull(responseInfo);
                Assert.Equal(followInfo.Id, responseInfo.Id);
                Assert.Equal(followInfo.Author.Id, responseInfo.Author.Id);
                Assert.Equal(followInfo.User.Id, responseInfo.User.Id);
            }

            [Fact]
            public async Task ShowNotExistingInfo()
            {
                var responseInfo = await AuthorsFollowingClient.Show(51523178);
                Assert.Null(responseInfo);
            }
        }
    }
}
