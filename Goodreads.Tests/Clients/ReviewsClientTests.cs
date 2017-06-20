using System;
using System.Text.RegularExpressions;
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
                var expectedId = 1700227480;
                var review = await ReviewsClient.GetById(expectedId);

                Assert.NotNull(review);
                Assert.Equal(review.Id, expectedId);
            }
        }

        public class TheGetByUserIdAndBookIdMethod : ReviewsClientTests
        {
            [Fact]
            public async Task ReturnsAReview()
            {
                var expectedBookId = 68428;
                var review = await ReviewsClient.GetByUserIdAndBookId(UserId, expectedBookId);

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
                var review = await ReviewsClient.GetByUserIdAndBookId(
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
                        // TODO: Goodreads is currently returning sorted lists wrong. Disable this
                        // assertion for now until they hopefully fix it in the future.
                        // Assert.True(previousReview.Book.AverageRating >= currentReview.Book.AverageRating);
                    }

                    previousReview = currentReview;
                }
            }
        }

        public class TheGetRecentReviewsForAllMembersMethod : ReviewsClientTests
        {
            [Fact]
            public async Task ReturnsReviews()
            {
                var reviews = await ReviewsClient.GetRecentReviewsForAllMembers();

                Assert.NotNull(reviews);
                Assert.True(reviews.Count > 0);
            }
        }

        public class TheCreateAndDeleteMethods : ReviewsClientTests
        {
            [Fact(Skip = "Possible issue with Goodreads review/destroy endpoint")]
            public async Task CreateAndDeleteAReview()
            {
                var reviewId = await ReviewsClient.Create(10790277);
                Assert.NotNull(reviewId);

                ////var result = await ReviewsClient.Delete(1700138017);
                ////Assert.True(result);
            }

            [Fact]
            public async Task ReturnsNullIfCannotCreate()
            {
                var reviewId = await ReviewsClient.Create(int.MaxValue);

                Assert.Null(reviewId);
            }
        }

        public class TheEditMethod : ReviewsClientTests
        {
            private readonly int EditReviewId = 1700227480;

            [Fact]
            public async Task EditRatingSucceeds()
            {
                var reviewBeforeEdit = await ReviewsClient.GetById(EditReviewId);
                var ratingBeforeEdit = reviewBeforeEdit.Rating;
                var expectedNewRating = ratingBeforeEdit == 5 ? 4 : 5;
                var result = await ReviewsClient.Edit(EditReviewId, rating: expectedNewRating);

                Assert.True(result);

                var reviewAfterEdit = await ReviewsClient.GetById(EditReviewId);
                var actualRatingAfterEdit = reviewAfterEdit.Rating;

                Assert.Equal(expectedNewRating, actualRatingAfterEdit);
            }

            [Fact]
            public async Task EditReviewTextSucceeds()
            {
                var reviewBeforeEdit = await ReviewsClient.GetById(EditReviewId);
                var textBeforeEdit = reviewBeforeEdit.Body.Trim();
                var match = Regex.Match(textBeforeEdit, @".*(\d+)");
                var testNumber = int.Parse(match.Groups[1].Value);
                var expectedNewText = textBeforeEdit.Replace(testNumber.ToString(), (testNumber + 1).ToString());

                var result = await ReviewsClient.Edit(EditReviewId, reviewText: expectedNewText);

                Assert.True(result);

                var reviewAfterEdit = await ReviewsClient.GetById(EditReviewId);
                var actualNewText = reviewAfterEdit.Body.Trim();

                Assert.Equal(expectedNewText, actualNewText);
            }

            [Fact]
            public async Task EditReadDateSucceeds()
            {
                var reviewBeforeEdit = await ReviewsClient.GetById(EditReviewId);
                var dateBeforeEdit = reviewBeforeEdit.DateRead;
                var expectedNewDate = dateBeforeEdit.Value.Date >= DateTime.UtcNow.Date ? DateTime.UtcNow.Date.AddDays(-7) : DateTime.UtcNow.Date;

                var result = await ReviewsClient.Edit(EditReviewId, dateRead: expectedNewDate);

                Assert.True(result);

                var reviewAfterEdit = await ReviewsClient.GetById(EditReviewId);
                var actualNewDate = reviewAfterEdit.DateRead;

                Assert.NotNull(actualNewDate);
                Assert.True(dateBeforeEdit.Value.Date != actualNewDate.Value.Date);
            }
        }
    }
}
