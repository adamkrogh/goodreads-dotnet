using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class CommentsClientTests
    {
        private readonly ICommentClient CommentClient;

        public CommentsClientTests()
        {
            CommentClient = Helper.GetAuthClient().Comments;
        }

        public class TheCreateCommentMethod : CommentsClientTests
        {
            [Fact(Skip = "Impossible to test because I can't remove comment using the Goodreads API. So I can't clean up a test suite.")]
            public void Create()
            {
            }
        }
    }
}
