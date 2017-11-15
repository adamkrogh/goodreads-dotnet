using System;
using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class AuthorsEndpointTests
    {
        private readonly IAuthorsEndpoint AuthorsEndpoint;

        public AuthorsEndpointTests()
        {
            AuthorsEndpoint = Helper.GetClient().Authors;
        }

        public class TheGetByAuthorIdMethod : AuthorsEndpointTests
        {
            [Fact]
            public async Task ReturnsAnAuthor()
            {
                var authorId = 38550;
                var author = await AuthorsEndpoint.GetByAuthorId(authorId);

                Assert.NotNull(author);
                Assert.Equal(author.Id, authorId);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var authorId = -1;
                var author = await AuthorsEndpoint.GetByAuthorId(authorId);

                Assert.Null(author);
            }
        }

        public class TheGetAuthorIdByNameMethod : AuthorsEndpointTests
        {
            [Fact]
            public async Task ReturnsAnAuthorId()
            {
                var authorName = "Brandon Sanderson";
                var expectedAuthorId = 38550;
                var actualAuthorId = await AuthorsEndpoint.GetAuthorIdByName(authorName);

                Assert.NotNull(actualAuthorId);
                Assert.Equal(actualAuthorId, expectedAuthorId);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var authorName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                var actualAuthorId = await AuthorsEndpoint.GetAuthorIdByName(authorName);

                Assert.Null(actualAuthorId);
            }
        }
    }
}
