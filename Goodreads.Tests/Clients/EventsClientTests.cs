using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class EventsClientTests
    {
        private readonly IEventsEndpoint EventsClient;

        public EventsClientTests()
        {
            EventsClient = Helper.GetAuthClient().Events;
        }

        public class TheGetEventsMethod : EventsClientTests
        {
            [Fact]
            public async Task GetNearest()
            {
                var events = await EventsClient.GetEvents();

                Assert.NotNull(events);
            }

            [Fact]
            public async Task GetByCriteria()
            {
                var events = await EventsClient.GetEvents(countryCode: "US", postalCode: 8540);

                Assert.NotNull(events);
            }
        }
    }
}
