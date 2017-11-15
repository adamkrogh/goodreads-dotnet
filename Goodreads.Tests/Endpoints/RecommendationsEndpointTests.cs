using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class RecommendationsEndpointTests
    {
        private readonly IOAuthRecommendationsEndpoint RecommendationsEndpoint;

        public RecommendationsEndpointTests()
        {
            RecommendationsEndpoint = Helper.GetAuthClient().Recommendations;
        }

        public class TheGetFriensUpdateMethod : RecommendationsEndpointTests
        {
            [Fact]
            public async Task GetRecommendations()
            {
                const int id = 26826409;
                var recommendation = await RecommendationsEndpoint.GetRecommendation(id);
                Assert.NotNull(recommendation);
                Assert.Equal(recommendation.Id, id);
            }
        }
    }
}
