using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests.Clients
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
    }
}
