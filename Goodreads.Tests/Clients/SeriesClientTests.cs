using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests.Clients
{
    public class SeriesClientTests
    {
        private readonly ISeriesClient SeriesClient;

        public SeriesClientTests()
        {
            SeriesClient = Helper.GetAuthClient().Series;
        }

        public class TheGetListByAuthorIdMethod : SeriesClientTests
        {
            [Fact]
            public async Task ReturnsSeries()
            {
                var series = await SeriesClient.GetListByAuthorId(38550);
                Assert.NotNull(series);
                Assert.True(series.Count > 0);
            }

            [Fact]
            public async Task ReturnsNullWhenIncorrectAuthor()
            {
                var series = await SeriesClient.GetListByAuthorId(int.MaxValue);
                Assert.Null(series);
            }
        }
    }
}
