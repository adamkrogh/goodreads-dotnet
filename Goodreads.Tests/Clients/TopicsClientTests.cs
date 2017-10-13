using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class TopicsClientTests
    {
        private readonly ITopicsClient TopicsClient;

        public TopicsClientTests()
        {
            TopicsClient = Helper.GetAuthClient().Topics;
        }

        public class TheGetInfoMethod : TopicsClientTests
        {
            [Fact]
            public async Task GetInfo()
            {
                const int topicId = 18915208;
                var topic = await TopicsClient.GetInfo(topicId);

                Assert.Equal(topicId, topic.Id);
                Assert.NotEmpty(topic.Title);
                Assert.NotNull(topic.Group);
                Assert.NotNull(topic.Folder);
            }
        }
    }
}
