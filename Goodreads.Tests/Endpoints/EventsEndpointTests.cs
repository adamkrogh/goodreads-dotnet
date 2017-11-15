using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class EventsEndpointTests
    {
        private readonly IEventsEndpoint EventsEndpoint;

        public EventsEndpointTests()
        {
            EventsEndpoint = Helper.GetAuthClient().Events;
        }

        public class TheGetEventsMethod : EventsEndpointTests
        {
            [Fact]
            public async Task GetNearest()
            {
                var events = await EventsEndpoint.GetEvents();

                Assert.NotNull(events);
            }

            [Fact]
            public async Task GetByCriteria()
            {
                var events = await EventsEndpoint.GetEvents(countryCode: "US", postalCode: 8540);

                Assert.NotNull(events);
            }
        }
    }
}
