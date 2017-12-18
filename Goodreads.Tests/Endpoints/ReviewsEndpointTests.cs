using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Goodreads.Clients;
using Goodreads.Models.Request;
using Goodreads.Models.Response;
using Xunit;

namespace Goodreads.Tests
{
    public class ReviewsEndpointTests
    {
        private readonly IOAuthReviewsEndpoint ReviewsEndpoint;
        private readonly long UserId;
        private readonly long ReviewsUserId;
        private readonly Random _rand = new Random();

        public ReviewsEndpointTests()
        {
            ReviewsEndpoint = Helper.GetAuthClient().Reviews;
            UserId = Helper.GetUserId();
            ReviewsUserId = 7284465;
        }

        public class TheGetByIdMethod : ReviewsEndpointTests
        {
            [Fact]
            public async Task ReturnsAReview()
            {
                int expectedId = 2153;
                var review = await ReviewsEndpoint.GetById(expectedId);

                Assert.NotNull(review);
                Assert.Equal(review.Id, expectedId);
            }
        }

        public class TheGetByUserIdAndBookIdMethod : ReviewsEndpointTests
        {
            [Fact]
            public async Task ReturnsAReview()
            {
                var expectedBookId = 68428;
                var review = await ReviewsEndpoint.GetByUserIdAndBookId(UserId, expectedBookId);

                Assert.NotNull(review);
                Assert.NotNull(review.Book);
                Assert.NotNull(review.User);
                Assert.Equal(review.User.Id, UserId);
                Assert.Equal(review.Book.Id, expectedBookId);
            }

            [Fact]
            public async Task ReturnsAReviewOnADifferentEdition()
            {
                var expectedBookId = 68428;
                var differentEditionBookId = 243272;
                var review = await ReviewsEndpoint.GetByUserIdAndBookId(
                    UserId,
                    differentEditionBookId,
                    findReviewOnDifferentEdition: true);

                Assert.NotNull(review);
                Assert.NotNull(review.Book);
                Assert.NotNull(review.User);
                Assert.Equal(review.User.Id, UserId);
                Assert.Equal(review.Book.Id, expectedBookId);
            }
        }

        public class TheGetListByUserMethod : ReviewsEndpointTests
        {
            [Fact]
            public async Task ReturnsReviews()
            {
                var reviews = await ReviewsEndpoint.GetListByUser(ReviewsUserId);

                Assert.NotNull(reviews);
            }

            [Fact]
            public async Task ReturnsCorrectPageSize()
            {
                var expectedPageSize = 42;
                var reviews = await ReviewsEndpoint.GetListByUser(ReviewsUserId, pageSize: expectedPageSize);

                Assert.NotNull(reviews);
                Assert.Equal(reviews.Pagination.End, expectedPageSize);
                Assert.Equal(reviews.List.Count, expectedPageSize);
            }

            [Fact]
            public async Task ReturnsSortedList()
            {
                var reviews = await ReviewsEndpoint.GetListByUser(
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
                        // TODO: Goodreads is currently returning sorted lists wrong. Disable this
                        // assertion for now until they hopefully fix it in the future.
                        // Assert.True(previousReview.Book.AverageRating >= currentReview.Book.AverageRating);
                    }

                    previousReview = currentReview;
                }
            }
        }

        public class TheGetRecentReviewsForAllMembersMethod : ReviewsEndpointTests
        {
            [Fact]
            public async Task ReturnsReviews()
            {
                var reviews = await ReviewsEndpoint.GetRecentReviewsForAllMembers();

                Assert.NotNull(reviews);
                Assert.True(reviews.Count > 0);
            }
        }

        public class TheCreateAndDeleteMethods : ReviewsEndpointTests
        {
            [Fact]
            public async Task ReturnsNullIfCannotCreate()
            {
                var reviewId = await ReviewsEndpoint.Create(int.MaxValue, string.Empty, null, null, null);

                Assert.Null(reviewId);
            }
        }

        public class TheEditMethod : ReviewsEndpointTests
        {
            [Fact]
            public async Task EditRatingSucceeds()
            {
                const long EditReviewId = 1700138017;
                var reviewBeforeEdit = await ReviewsEndpoint.GetById(EditReviewId);
                var ratingBeforeEdit = reviewBeforeEdit.Rating;
                var expectedNewRating = ratingBeforeEdit == 5 ? 4 : 5;
                var result = await ReviewsEndpoint.Edit(EditReviewId, string.Empty, expectedNewRating, null, null);

                Assert.True(result);

                var reviewAfterEdit = await ReviewsEndpoint.GetById(EditReviewId);
                var actualRatingAfterEdit = reviewAfterEdit.Rating;

                Assert.Equal(expectedNewRating, actualRatingAfterEdit);
            }

            [Fact]
            public async Task EditReviewTextSucceeds()
            {
                const long EditReviewId = 2175139156;
                var expectedNewText = "test_" + _rand.Next(); 

                var result = await ReviewsEndpoint.Edit(EditReviewId, expectedNewText, null, null, null);

                Assert.True(result);

                var reviewAfterEdit = await ReviewsEndpoint.GetById(EditReviewId);
                var actualNewText = reviewAfterEdit.Body.Trim();

                Assert.Equal(expectedNewText, actualNewText);
            }

            [Fact]
            public async Task EditReadDateSucceeds()
            {
                const long EditReviewId = 2034600714;
                var reviewBeforeEdit = await ReviewsEndpoint.GetById(EditReviewId);
                var dateBeforeEdit = reviewBeforeEdit.DateRead;
                var expectedNewDate =
                    dateBeforeEdit.GetValueOrDefault().Date >= DateTime.UtcNow.Date
                    ? DateTime.UtcNow.Date.AddDays(-7)
                    : DateTime.UtcNow.Date;

                var result = await ReviewsEndpoint.Edit(EditReviewId, string.Empty, null, expectedNewDate, null);

                Assert.True(result);

                var reviewAfterEdit = await ReviewsEndpoint.GetById(EditReviewId);
                var actualNewDate = reviewAfterEdit.DateRead;

                Assert.NotNull(actualNewDate);
                Assert.True(dateBeforeEdit.Value.Date != actualNewDate.Value.Date);
            }
        }
    }
}
