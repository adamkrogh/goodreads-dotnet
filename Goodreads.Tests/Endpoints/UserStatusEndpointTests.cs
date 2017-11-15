using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class UserStatusEndpointTests
    {
        private readonly IOAuthUserStatusesEndpoint UserStatusesEndpoint;

        public UserStatusEndpointTests()
        {
            UserStatusesEndpoint = Helper.GetAuthClient().UserStatuses;
        }

        public class TheGetRecentStatusesMethod : UserStatusEndpointTests
        {
            [Fact]
            public async Task GetRecentUsersStatuses()
            {
                var statuses = await UserStatusesEndpoint.GetRecentUsersStatuses();

                Assert.NotNull(statuses);
                Assert.NotEmpty(statuses);
            }
        }

        public class TheGetUserStatusMethod : UserStatusEndpointTests
        {
            [Fact]
            public async Task GetUserStatus()
            {
                const int id = 138141943;
                var status = await UserStatusesEndpoint.GetUserStatus(id);

                Assert.NotNull(status);
                Assert.Equal(status.Id, id);
                Assert.NotNull(status.User);
                Assert.NotNull(status.Book);
            }
        }

        public class TheCreateUserStatusMethod : UserStatusEndpointTests
        {
            [Fact]
            public async Task Create()
            {
                const int bookId = 186;
                var id = await UserStatusesEndpoint.Create(bookId, percent: 42, comment: "Really cool!");

                // clean up status
                await UserStatusesEndpoint.Delete(id);

                Assert.NotEqual(default(long), id);
            }
        }

        public class TheDeleteUserStatusMethod : UserStatusEndpointTests
        {
            [Fact]
            public async Task Delete()
            {
                // arrange status
                var id = await UserStatusesEndpoint.Create(18667945, percent: 1, comment: "Not bad");

                var result = await UserStatusesEndpoint.Delete(id);

                Assert.True(result);
            }
        }
    }
}
