using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class NotificationEndpointTests
    {
        private readonly IOAuthNotificationsEndpoint NotificationsEndpoint;

        public NotificationEndpointTests()
        {
            NotificationsEndpoint = Helper.GetAuthClient().Notifications;
        }

        public class TheAddFriendMethod : NotificationEndpointTests
        {
            [Fact]
            public async Task GetNotificationsMethod()
            {
                var notifications = await NotificationsEndpoint.GetNotifications();

                Assert.NotNull(notifications);
                Assert.NotNull(notifications.List);
                Assert.NotEmpty(notifications.List);
            }
        }
    }
}
