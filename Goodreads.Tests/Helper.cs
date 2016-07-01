namespace Goodreads.Tests
{
    public static class Helper
    {
        public static IGoodreadsClient GetClient()
        {
            // TODO: Move API credentials out to environment variables, and reset them on my account...
            return new GoodreadsClient("epWZe3lcFrBCLt8VKoXtBg", "wUHnxrgwi7EbfjI01O9JD53ODRK1IOXHC903jkH4gAM");
        }
    }
}
