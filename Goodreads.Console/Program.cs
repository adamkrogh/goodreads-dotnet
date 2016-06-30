namespace Goodreads.Console
{
    using System.Threading.Tasks;
    using Models.Request;
    using Console = System.Console;

    internal class Program
    {
        internal static void Main(string[] args)
        {
            var client = new GoodreadsClient("epWZe3lcFrBCLt8VKoXtBg", "wUHnxrgwi7EbfjI01O9JD53ODRK1IOXHC903jkH4gAM");

            GetAuthor(client);
            GetBooksByAuthor(client);
            GetBook(client);
            GetShelves(client);
            SearchBooks(client);

            var authClient = new GoodreadsClient(
                "epWZe3lcFrBCLt8VKoXtBg",
                "wUHnxrgwi7EbfjI01O9JD53ODRK1IOXHC903jkH4gAM",
                "insert_oauth_token_here",
                "insert_oauth_token_secret_here");

            GetFriends(authClient);

            Console.ReadKey();
        }

        private static void GetAuthor(GoodreadsClient client)
        {
            var authorId = 1624;
            var authorTask = client.Authors.Get(authorId);
            Task.WaitAll(authorTask);
            var author = authorTask.Result;

            if (author != null)
            {
                Console.Out.WriteLine(author.Name);
            }
            else
            {
                Console.Out.WriteLine("Author with Id: {0} was not found.", authorId);
            }
        }

        private static void GetBooksByAuthor(GoodreadsClient client)
        {
            var authorId = 1624;
            var authorBooksTask = client.Books.GetListByAuthorId(authorId, page: 2);
            Task.WaitAll(authorBooksTask);
            var books = authorBooksTask.Result;

            if (books != null)
            {
                Console.Out.WriteLine(books.Pagination.TotalItems + " books found");
            }
        }

        private static void GetBook(GoodreadsClient client)
        {
            var isbn = "0441172717";
            var bookTask = client.Books.GetByIsbn(isbn);
            Task.WaitAll(bookTask);
            var book = bookTask.Result;

            if (book != null)
            {
                Console.Out.WriteLine(book.Title);
            }
            else
            {
                Console.Out.WriteLine("Book with ISBN: {0} was not found.", isbn);
            }
        }

        private static void GetShelves(GoodreadsClient client)
        {
            var userId = 2863914;
            var shelfTask = client.Shelves.GetUserShelves(userId, page: 2);
            Task.WaitAll(shelfTask);
            var shelves = shelfTask.Result;

            if (shelves != null)
            {
                Console.Out.WriteLine(shelves.Pagination.TotalItems + " shelves found");
            }
        }

        private static void SearchBooks(GoodreadsClient client)
        {
            var searchTerm = "stormlight";
            var searchTask = client.Books.Search(searchTerm, page: 1, searchField: BookSearchField.Title);
            Task.WaitAll(searchTask);
            var results = searchTask.Result;

            if (results != null)
            {
                Console.Out.WriteLine(results.Pagination.TotalItems + " results found");
            }
        }

        private static void GetFriends(GoodreadsClient client)
        {
            var userId = 7284465;
            var friendsTask = client.Users.GetListOfFriends(userId, 1, SortFriendsList.FirstName);
            Task.WaitAll(friendsTask);
            var friends = friendsTask.Result;

            if (friends != null)
            {
                Console.Out.WriteLine(friends.Pagination.TotalItems + " friends found");
            }
        }
    }
}
