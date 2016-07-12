using System.Threading.Tasks;
using Goodreads.Clients;
using Goodreads.Models.Request;
using Goodreads.Models.Response;
using Xunit;

namespace Goodreads.Tests.Clients
{
    public class ReviewsClientTests
    {
        private readonly IReviewsClient ReviewsClient;
        private readonly int UserId;
        private readonly int ReviewsUserId;

        public ReviewsClientTests()
        {
            ReviewsClient = Helper.GetAuthClient().Reviews;
            UserId = Helper.GetUserId();
            ReviewsUserId = 7284465;
        }

        public class TheGetListByUserMethod : ReviewsClientTests
        {
            [Fact]
            public async Task ReturnsBooks()
            {
                var books = await ReviewsClient.GetListByUser(ReviewsUserId);

                Assert.NotNull(books);
            }

            [Fact]
            public async Task ReturnsCorrectPageSize()
            {
                var expectedPageSize = 42;
                var books = await ReviewsClient.GetListByUser(ReviewsUserId, pageSize: expectedPageSize);

                Assert.NotNull(books);
                Assert.Equal(books.Pagination.End, expectedPageSize);
                Assert.Equal(books.List.Count, expectedPageSize);
            }

            [Fact]
            public async Task ReturnsSortedList()
            {
                var reviews = await ReviewsClient.GetListByUser(
                    ReviewsUserId,
                    sort: SortReviewsList.AverageRating,
                    order: Order.Descending);

                Assert.NotNull(reviews);

                Review previousReview = null;
                foreach (var currentReview in reviews.List)
                {
                    Assert.NotNull(currentReview);
                    Assert.NotNull(currentReview.Book);
                    Assert.NotNull(currentReview.Book.AverageRating);

                    if (previousReview != null)
                    {
                        Assert.True(previousReview.Book.AverageRating >= currentReview.Book.AverageRating);
                    }

                    previousReview = currentReview;
                }
            }
        }
    }
}
