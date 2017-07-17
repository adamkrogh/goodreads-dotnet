using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class QuotesClientTests
    {
        private readonly IQuotesClient QuotesClient;

        public QuotesClientTests()
        {
            QuotesClient = Helper.GetAuthClient().Quotes;
        }

        public class TheAddMethod : QuotesClientTests
        {
            [Fact(Skip = "Impossible to test because I can't destroy a quote using the Goodreads API. So I can't clean up a test suite.")]
            public void AddQuote()
            {
            }
        }
    }
}
