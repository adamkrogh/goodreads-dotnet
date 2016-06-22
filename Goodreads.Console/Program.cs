namespace Goodreads.Console
{
    using System.Threading.Tasks;
    using Console = System.Console;

    internal class Program
    {
        internal static void Main(string[] args)
        {
            var client = new GoodreadsClient("epWZe3lcFrBCLt8VKoXtBg");
            var authorTask = client.Authors.Get(1624);

            Task.WaitAll(authorTask);

            var author = authorTask.Result;

            Console.Out.WriteLine(author.Name);

            Console.ReadKey();
        }
    }
}
