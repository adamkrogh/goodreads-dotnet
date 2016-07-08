using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests.Clients
{
    public class ReviewsClientTests
    {
        private readonly IReviewsClient ReviewsClient;
        private readonly int UserId;

        public ReviewsClientTests()
        {
            ReviewsClient = Helper.GetAuthClient().Reviews;
            UserId = Helper.GetUserId();
        }

        public class TheGetListByUserMethod : ReviewsClientTests
        {
            [Fact]
            public async Task ReturnsBooks()
            {
                var books = await ReviewsClient.GetListByUser(UserId);

                Assert.NotNull(books);
            }
        }
    }
}
