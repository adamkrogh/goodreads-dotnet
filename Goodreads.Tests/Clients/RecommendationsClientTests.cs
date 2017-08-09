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
                var recommendation = await RecommendationsClient.GetRecommendation(id);
                Assert.NotNull(recommendation);
                Assert.Equal(recommendation.Id, id);
            }
        }
    }
}
