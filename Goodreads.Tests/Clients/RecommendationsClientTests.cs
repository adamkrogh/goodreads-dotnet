using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class RecommendationsClientTests
    {
        private readonly IRecommendationsClient RecommendationsClient;

        public RecommendationsClientTests()
        {
            RecommendationsClient = Helper.GetAuthClient().Recommendations;
        }

        public class TheGetFriensUpdateMethod : RecommendationsClientTests
        {
            [Fact]
            public async Task GetRecommendations()
            {
                const int id = 26826409;
                var recommendation = await RecommendationsClient.GetRecommendation(26826409);
                Assert.NotNull(recommendation);
                Assert.Equal(id, recommendation.Id);
            }
        }
    }
}
