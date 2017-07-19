using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class UserStatusClientTests
    {
        private readonly IUserStatusesClient UserStatuses;

        public UserStatusClientTests()
        {
            UserStatuses = Helper.GetAuthClient().UserStatuses;
        }

        public class TheGetRecentStatusesMethod : UserStatusClientTests
        {
            [Fact]
            public async Task GetRecentUsersStatuses()
            {
                var statuses = await UserStatuses.GetRecentUsersStatuses();

                Assert.NotNull(statuses);
                Assert.NotEmpty(statuses);
            }
        }

        public class TheGetUserStatusMethod : UserStatusClientTests
        {
            [Fact]
            public async Task GetUserStatus()
            {
                const int id = 138141943;
                var status = await UserStatuses.GetUserStatus(id);

                Assert.NotNull(status);
                Assert.Equal(status.Id, id);
                Assert.NotNull(status.User);
                Assert.NotNull(status.Book);
            }
        }
    }
}
