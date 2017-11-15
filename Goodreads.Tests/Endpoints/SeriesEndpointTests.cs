using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class SeriesEndpointTests
    {
        private readonly ISeriesEndpoint SeriesEndpoint;

        public SeriesEndpointTests()
        {
            SeriesEndpoint = Helper.GetAuthClient().Series;
        }

        public class TheGetListByAuthorIdMethod : SeriesEndpointTests
        {
            [Fact]
            public async Task ReturnsSeries()
            {
                var series = await SeriesEndpoint.GetListByAuthorId(38550);

                Assert.NotNull(series);
                Assert.True(series.Count > 0);
            }

            [Fact]
            public async Task ReturnsNullWhenIncorrectAuthor()
            {
                var series = await SeriesEndpoint.GetListByAuthorId(int.MaxValue);

                Assert.Null(series);
            }
        }

        public class TheGetListByWorkIdMethod : SeriesEndpointTests
        {
            [Fact]
            public async Task ReturnsSeries()
            {
                var series = await SeriesEndpoint.GetListByWorkId(8134945);

                Assert.NotNull(series);
                Assert.True(series.Count > 0);
            }

            [Fact]
            public async Task ReturnsNullWhenIncorrectWork()
            {
                var series = await SeriesEndpoint.GetListByWorkId(int.MaxValue);

                Assert.Null(series);
            }
        }

        public class TheGetByIdMethod : SeriesEndpointTests
        {
            [Fact]
            public async Task ReturnsSeriesInformation()
            {
                var expectedSeriesId = 49075;
                var series = await SeriesEndpoint.GetById(expectedSeriesId);

                Assert.NotNull(series);
                Assert.Equal(expectedSeriesId, series.Id);
                Assert.NotNull(series.Works);
                Assert.True(series.Works.Count > 0);
            }

            [Fact]
            public async Task ReturnsNullWhenIncorrectSeriesId()
            {
                var series = await SeriesEndpoint.GetById(int.MaxValue);

                Assert.Null(series);
            }
        }
    }
}
