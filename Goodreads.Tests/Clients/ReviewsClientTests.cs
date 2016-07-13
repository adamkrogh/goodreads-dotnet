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

        public class TheGetByIdMethod : ReviewsClientTests
        {
            [Fact]
            public async Task ReturnsAReview()
            {
                var expectedId = 1690356266;
                var review = await ReviewsClient.GetById(expectedId);

                Assert.NotNull(review);
                Assert.Equal(review.Id, expectedId);
            }
        }

        public class TheGetListByUserMethod : ReviewsClientTests
        {
            [Fact]
            public async Task ReturnsReviews()
            {
                var reviews = await ReviewsClient.GetListByUser(ReviewsUserId);

                Assert.NotNull(reviews);
            }

            [Fact]
            public async Task ReturnsCorrectPageSize()
            {
                var expectedPageSize = 42;
                var reviews = await ReviewsClient.GetListByUser(ReviewsUserId, pageSize: expectedPageSize);

                Assert.NotNull(reviews);
                Assert.Equal(reviews.Pagination.End, expectedPageSize);
                Assert.Equal(reviews.List.Count, expectedPageSize);
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
