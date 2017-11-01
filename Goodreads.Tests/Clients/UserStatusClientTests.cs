using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class UserStatusClientTests
    {
        private readonly IOAuthUserStatusesEndpoint UserStatuses;

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

        public class TheCreateUserStatusMethod : UserStatusClientTests
        {
            [Fact]
            public async Task Create()
            {
                const int bookId = 186;
                var id = await UserStatuses.Create(bookId, percent: 42, comment: "Really cool!");

                // clean up status
                await UserStatuses.Delete(id);

                Assert.NotEqual(default(long), id);
            }
        }

        public class TheDeleteUserStatusMethod : UserStatusClientTests
        {
            [Fact]
            public async Task Delete()
            {
                // arrange status
                var id = await UserStatuses.Create(18667945, percent: 1, comment: "Not bad");

                var result = await UserStatuses.Delete(id);

                Assert.True(result);
            }
        }
    }
}
