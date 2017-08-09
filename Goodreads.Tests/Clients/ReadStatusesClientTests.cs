using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class ReadStatusesClientTests
    {
        private readonly IReadStatusesClient ReadStatusesClient;

        public ReadStatusesClientTests()
        {
            ReadStatusesClient = Helper.GetAuthClient().ReadStatuses;
        }

        public class TheGetFriensUpdateMethod : ReadStatusesClientTests
        {
            [Fact]
            public async Task GetReadStatus()
            {
                const int id = 1894518811;
                var status = await ReadStatusesClient.GetReadStatus(id);
                Assert.NotNull(status);
                Assert.Equal(status.Id, id);
            }
        }
    }
}
