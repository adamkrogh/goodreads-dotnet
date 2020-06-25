using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class BooksEndpointTests
    {
        private readonly IOAuthBooksEndpoint BooksEndpoint;

        public BooksEndpointTests()
        {
            BooksEndpoint = Helper.GetAuthClient().Books;
        }

        public class TheGetByIsbnMethod : BooksEndpointTests
        {
            [Fact]
            public async Task ReturnsABook()
            {
                var isbn = "0765326353";
                var bookId = 7235533;
                var book = await BooksEndpoint.GetByIsbn(isbn);

                Assert.NotNull(book);
                Assert.Equal(book.Isbn, isbn);
                Assert.Equal(book.Id, bookId);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var book = await BooksEndpoint.GetByIsbn("test");

                Assert.Null(book);
            }
        }

        public class TheGetByBookIdMethod : BooksEndpointTests
        {
            [Fact]
            public async Task ReturnsABook()
            {
                var bookId = 7235533;
                var isbn = "0765326353";
                var book = await BooksEndpoint.GetByBookId(bookId);

                Assert.NotNull(book);
                Assert.Equal(book.Id, bookId);
                Assert.Equal(book.Isbn, isbn);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var book = await BooksEndpoint.GetByBookId(int.MaxValue);

                Assert.Null(book);
            }
        }

        public class TheGetByTitleMethod : BooksEndpointTests
        {
            [Fact]
            public async Task ReturnsABook()
            {
                var title = "The Way of Kings";
                var bookId = 7235533;
                var isbn = "0765326353";
                var book = await BooksEndpoint.GetByTitle(title);

                Assert.NotNull(book);
                Assert.Equal(book.Id, bookId);
                Assert.Equal(book.Isbn, isbn);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                // Hopefully no one will write a book with this title in the future...
                var book = await BooksEndpoint.GetByTitle("asasdasd123123123");

                Assert.Null(book);
            }
        }

        public class TheGetListByAuthorIdMethod : BooksEndpointTests
        {
            [Fact]
            public async Task ReturnsBooks()
            {
                var authorId = 38550;
                var books = await BooksEndpoint.GetListByAuthorId(authorId);

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
                Assert.True(books.Pagination.TotalItems > 0);
                Assert.Contains(books.List.First().Authors, x => x.Id == authorId);
            }

            [Fact]
            public async Task ReturnsASecondPage()
            {
                var authorId = 38550;
                var books = await BooksEndpoint.GetListByAuthorId(authorId, page: 2);

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
                Assert.True(books.Pagination.TotalItems > 0);
                Assert.True(books.Pagination.Start == 31);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var authorId = -1;
                var books = await BooksEndpoint.GetListByAuthorId(authorId);

                Assert.Null(books);
            }
        }

        public class TheSearchMethod : BooksEndpointTests
        {
            [Fact]
            public async Task ReturnsResults()
            {
                var books = await BooksEndpoint.Search("stormlight");

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
                Assert.True(books.Pagination.TotalItems > 0);
            }

            [Fact]
            public async Task ReturnsASecondPage()
            {
                var books = await BooksEndpoint.Search("stormlight", page: 2);

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
                Assert.True(books.Pagination.TotalItems > 0);
                Assert.True(books.Pagination.Start > 1);
            }

            [Fact]
            public async Task GetsTheAverageRating()
            {
                var books = await BooksEndpoint.Search("stormlight", page: 1);

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
                Assert.True(books.Pagination.TotalItems > 0);
                Assert.True(books.Pagination.Start > 1);
                Assert.True(books.List[0].AverageRating > 0);
            }
        }

        public class TheGetBookIdForIsbnMethod : BooksEndpointTests
        {
            [Fact]
            public async Task ReturnsABookId()
            {
                var isbn = "0765326353";
                var bookId = await BooksEndpoint.GetBookIdForIsbn(isbn);

                Assert.NotNull(bookId);
                Assert.Equal(7235533, bookId);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var isbn = "test";
                var bookId = await BooksEndpoint.GetBookIdForIsbn(isbn);

                Assert.Null(bookId);
            }
        }

        public class TheGetBookIdsForIsbnsMethod : BooksEndpointTests
        {
            [Fact]
            public async Task ReturnsBookIds()
            {
                var isbns = new List<string> { "0765326353", "9780765326362" };
                var expectedBookIds = new List<int?> { 7235533, 17332218 };
                var actualBookIds = await BooksEndpoint.GetBookIdsForIsbns(isbns);

                Assert.NotNull(actualBookIds);
                Assert.Equal(actualBookIds.Count, isbns.Count);
                Assert.Equal(actualBookIds.Count, expectedBookIds.Count);

                for (var i = 0; i < actualBookIds.Count; i++)
                {
                    Assert.Equal(actualBookIds[i], expectedBookIds[i]);
                }
            }

            [Fact]
            public async Task HandlesMissingIsbns()
            {
                var isbns = new List<string> { "0765326353", "missing", "9780765326362" };
                var expectedBookIds = new List<int?> { 7235533, null, 17332218 };
                var actualBookIds = await BooksEndpoint.GetBookIdsForIsbns(isbns);

                Assert.NotNull(actualBookIds);
                Assert.Equal(actualBookIds.Count, isbns.Count);
                Assert.Equal(actualBookIds.Count, expectedBookIds.Count);

                for (var i = 0; i < actualBookIds.Count; i++)
                {
                    Assert.Equal(actualBookIds[i], expectedBookIds[i]);
                }
            }
        }

        public class TheGetWorkIdsForBookIdsMethod : BooksEndpointTests
        {
            [Fact]
            public async Task ReturnsWorkIds()
            {
                var bookIds = new List<long> { 7235533, 17332218 };
                var expectedWorkIds = new List<long> { 8134945, 16482835 };
                var actualWorkIds = await BooksEndpoint.GetWorkIdsForBookIds(bookIds);

                Assert.NotNull(actualWorkIds);
                Assert.Equal(actualWorkIds.Count, expectedWorkIds.Count);
                Assert.Equal(actualWorkIds.Count, expectedWorkIds.Count);

                for (var i = 0; i < actualWorkIds.Count; i++)
                {
                    Assert.Equal(actualWorkIds[i], expectedWorkIds[i]);
                }
            }

            [Fact]
            public async Task HandlesMissingBookIds()
            {
                var bookIds = new List<long> { 7235533, int.MaxValue, 17332218 };
                var expectedWorkIds = new List<long?> { 8134945, null, 16482835 };
                var actualWorkIds = await BooksEndpoint.GetWorkIdsForBookIds(bookIds);

                Assert.NotNull(actualWorkIds);
                Assert.Equal(actualWorkIds.Count, expectedWorkIds.Count);
                Assert.Equal(actualWorkIds.Count, expectedWorkIds.Count);

                for (var i = 0; i < actualWorkIds.Count; i++)
                {
                    Assert.Equal(actualWorkIds[i], expectedWorkIds[i]);
                }
            }
        }

        public class TheGetReviewStatsForIsbnsMethod : BooksEndpointTests
        {
            [Fact]
            public async Task ReturnsStats()
            {
                var isbns = new List<string> { "0765326353", "9780765326362" };
                var reviewStats = await BooksEndpoint.GetReviewStatsForIsbns(isbns);

                Assert.NotNull(reviewStats);
                Assert.Equal(reviewStats.Count, isbns.Count);
            }

            [Fact]
            public async Task OnlyReturnsStatsForFoundIsbns()
            {
                var isbns = new List<string> { "0765326353", "missing", "9780765326362" };
                var reviewStats = await BooksEndpoint.GetReviewStatsForIsbns(isbns);

                Assert.NotNull(reviewStats);
                Assert.Equal(2, reviewStats.Count);
            }
        }
    }
}
