using System.Threading.Tasks;
using Goodreads.Clients;
using Goodreads.Models.Request;
using Xunit;

namespace Goodreads.Tests
{
    public class RatingClientTests
    {
        private readonly IRatingClient RatingClient;

        public RatingClientTests()
        {
            RatingClient = Helper.GetAuthClient().Ratings;
        }

        public class TheLikeResourceMethod : RatingClientTests
        {
            [Fact]
            public async Task LikeResource()
            {
                const int id = 466169573;
                var type = ResourceType.Review;

                var result = await RatingClient.LikeResource(id, type);

                Assert.True(result);
            }
        }
    }
}
