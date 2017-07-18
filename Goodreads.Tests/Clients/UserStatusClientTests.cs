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
    }
}
