namespace Goodreads.Console
{
    using System.Threading.Tasks;
    using Console = System.Console;

    internal class Program
    {
        internal static void Main(string[] args)
        {
            var client = new GoodreadsClient("epWZe3lcFrBCLt8VKoXtBg");

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

            Console.ReadKey();
        }
    }
}
