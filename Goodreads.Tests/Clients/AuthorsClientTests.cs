using System;
using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class AuthorsClientTests
    {
        private readonly IAuthorsClient AuthorsClient;

        public AuthorsClientTests()
        {
            AuthorsClient = Helper.GetClient().Authors;
        }

        public class TheGetByAuthorIdMethod : AuthorsClientTests
        {
            [Fact]
            public async Task ReturnsAnAuthor()
            {
                var authorId = 38550;
                var author = await AuthorsClient.GetByAuthorId(authorId);

                Assert.NotNull(author);
                Assert.Equal(author.Id, authorId);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var authorId = -1;
                var author = await AuthorsClient.GetByAuthorId(authorId);

                Assert.Null(author);
            }
        }

        public class TheGetAuthorIdByNameMethod : AuthorsClientTests
        {
            [Fact]
            public async Task ReturnsAnAuthorId()
            {
                var authorName = "Brandon Sanderson";
                var expectedAuthorId = 38550;
                var actualAuthorId = await AuthorsClient.GetAuthorIdByName(authorName);

                Assert.NotNull(actualAuthorId);
                Assert.Equal(actualAuthorId, expectedAuthorId);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var authorName = Guid.NewGuid().ToString().Replace("-", string.Empty);
                var actualAuthorId = await AuthorsClient.GetAuthorIdByName(authorName);

                Assert.Null(actualAuthorId);
            }
        }
    }
}
