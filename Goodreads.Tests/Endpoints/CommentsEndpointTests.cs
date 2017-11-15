using System.Threading.Tasks;
using Goodreads.Clients;
using Goodreads.Models.Request;
using Xunit;

namespace Goodreads.Tests
{
    public class CommentsEndpointTests
    {
        private readonly ICommentsEndpoint CommentsEndpoint;

        public CommentsEndpointTests()
        {
            CommentsEndpoint = Helper.GetAuthClient().Comments;
        }

        public class TheGetAllCommentsMethod : CommentsEndpointTests
        {
            [Fact]
            public async Task GetAll()
            {
                const int resourceId = 48510472;
                var type = ResourceType.OwnedBook;

                var comments = await CommentsEndpoint.GetAll(resourceId, type);

                Assert.NotNull(comments);
                Assert.NotEmpty(comments.List);
            }
        }
    }
}
