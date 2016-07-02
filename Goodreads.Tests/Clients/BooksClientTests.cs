using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class BooksClientsTests
    {
        private readonly IBooksClient BooksClient;

        public BooksClientsTests()
        {
            BooksClient = Helper.GetClient().Books;
        }

        public class TheGetByIsbnMethod : BooksClientsTests
        {
            [Fact]
            public async Task ReturnsABook()
            {
                var isbn = "0765326353";
                var bookId = 7235533;
                var book = await BooksClient.GetByIsbn(isbn);

                Assert.NotNull(book);
                Assert.Equal(book.Isbn, isbn);
                Assert.Equal(book.Id, bookId);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var book = await BooksClient.GetByIsbn("test");

                Assert.Null(book);
            }
        }

        public class TheGetListByAuthorIdMethod : BooksClientsTests
        {
            [Fact]
            public async Task ReturnsBooks()
            {
                var authorId = 38550;
                var books = await BooksClient.GetListByAuthorId(authorId);

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
                Assert.True(books.Pagination.TotalItems > 0);
                Assert.True(books.List.First().Authors.Any(x => x.Id == authorId));
            }

            [Fact]
            public async Task ReturnsASecondPage()
            {
                var authorId = 38550;
                var books = await BooksClient.GetListByAuthorId(authorId, page: 2);

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
                Assert.True(books.Pagination.TotalItems > 0);
                Assert.True(books.Pagination.CurrentPage == 2);
                Assert.True(books.Pagination.Start == 31);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var authorId = -1;
                var books = await BooksClient.GetListByAuthorId(authorId);

                Assert.Null(books);
            }
        }

        public class TheSearchMethod : BooksClientsTests
        {
            [Fact]
            public async Task ReturnsResults()
            {
                var books = await BooksClient.Search("stormlight");

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
                Assert.True(books.Pagination.TotalItems > 0);
            }

            [Fact]
            public async Task ReturnsASecondPage()
            {
                var books = await BooksClient.Search("stormlight", page: 2);

                Assert.NotNull(books);
                Assert.NotEmpty(books.List);
                Assert.True(books.Pagination.TotalItems > 0);
                Assert.True(books.Pagination.CurrentPage == 2);
                Assert.True(books.Pagination.Start == 21);
            }
        }

        public class TheGetBookIdForIsbnMethod : BooksClientsTests
        {
            [Fact]
            public async Task ReturnsABookId()
            {
                var isbn = "0765326353";
                var bookId = await BooksClient.GetBookIdForIsbn(isbn);

                Assert.NotNull(bookId);
                Assert.Equal(bookId, 7235533);
            }

            [Fact]
            public async Task ReturnsNullIfNotFound()
            {
                var isbn = "test";
                var bookId = await BooksClient.GetBookIdForIsbn(isbn);

                Assert.Null(bookId);
            }
        }

        public class TheGetBookIdsForIsbnsMethod : BooksClientsTests
        {
            [Fact]
            public async Task ReturnsBookIds()
            {
                var isbns = new List<string> { "0765326353", "9780765326362" };
                var expectedBookIds = new List<int?> { 7235533, 17332218 };
                var actualBookIds = await BooksClient.GetBookIdsForIsbns(isbns);

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
                var actualBookIds = await BooksClient.GetBookIdsForIsbns(isbns);

                Assert.NotNull(actualBookIds);
                Assert.Equal(actualBookIds.Count, isbns.Count);
                Assert.Equal(actualBookIds.Count, expectedBookIds.Count);

                for (var i = 0; i < actualBookIds.Count; i++)
                {
                    Assert.Equal(actualBookIds[i], expectedBookIds[i]);
                }
            }
        }

        public class TheGetWorkIdsForBookIdsMethod : BooksClientsTests
        {
            [Fact]
            public async Task ReturnsWorkIds()
            {
                var bookIds = new List<int> { 7235533, 17332218 };
                var expectedWorkIds = new List<int> { 8134945, 16482835 };
                var actualWorkIds = await BooksClient.GetWorkIdsForBookIds(bookIds);

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
                var bookIds = new List<int> { 7235533, int.MaxValue, 17332218 };
                var expectedWorkIds = new List<int?> { 8134945, null, 16482835 };
                var actualWorkIds = await BooksClient.GetWorkIdsForBookIds(bookIds);

                Assert.NotNull(actualWorkIds);
                Assert.Equal(actualWorkIds.Count, expectedWorkIds.Count);
                Assert.Equal(actualWorkIds.Count, expectedWorkIds.Count);

                for (var i = 0; i < actualWorkIds.Count; i++)
                {
                    Assert.Equal(actualWorkIds[i], expectedWorkIds[i]);
                }
            }
        }
    }
}
