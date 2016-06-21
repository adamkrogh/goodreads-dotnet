namespace Goodreads.Console
{
    using System.Threading.Tasks;
    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            var client = new GoodreadsClient("");
            var authorTask = client.Authors.Get(38550);

            Task.WaitAll(authorTask);

            var author = authorTask.Result;

            Console.Out.WriteLine(author.Name);
            Console.ReadKey();
        }
    }
}
