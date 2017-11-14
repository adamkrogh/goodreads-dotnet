using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class NotificationClientTests
    {
        private readonly IOAuthNotificationsEndpoint NotificationsClient;

        public NotificationClientTests()
        {
            NotificationsClient = Helper.GetAuthClient().Notifications;
        }

        public class TheAddFriendMethod : NotificationClientTests
        {
            [Fact]
            public async Task GetNotificationsMethod()
            {
                var notifications = await NotificationsClient.GetNotifications();

                Assert.NotNull(notifications);
                Assert.NotNull(notifications.List);
                Assert.NotEmpty(notifications.List);
            }
        }
    }
}
