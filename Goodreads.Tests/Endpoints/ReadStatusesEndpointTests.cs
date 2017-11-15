using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class ReadStatusesEndpointTests
    {
        private readonly IReadStatusesEndpoint ReadStatusesEndpoint;

        public ReadStatusesEndpointTests()
        {
            ReadStatusesEndpoint = Helper.GetAuthClient().ReadStatuses;
        }

        public class TheGetFriensUpdateMethod : ReadStatusesEndpointTests
        {
            [Fact]
            public async Task GetReadStatus()
            {
                const int id = 1894518811;
                var status = await ReadStatusesEndpoint.GetReadStatus(id);
                Assert.NotNull(status);
                Assert.Equal(status.Id, id);
            }
        }
    }
}
