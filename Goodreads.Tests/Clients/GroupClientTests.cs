using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class GroupClientTests
    {
        private readonly IGroupsEndpoint GroupClient;

        public GroupClientTests()
        {
            GroupClient = Helper.GetAuthClient().Groups;
        }

        public class TheGetListByUserMethod : GroupClientTests
        {
            [Fact]
            public async Task GetListByUserToGroup()
            {
                var list = await GroupClient.GetListByUser(690273);

                Assert.NotNull(list);
            }
        }

        public class TheGetGroupsMethod : GroupClientTests
        {
            [Fact]
            public async Task GetGroups()
            {
                var groups = await GroupClient.GetGroups("English");

                Assert.NotNull(groups);
                Assert.NotNull(groups.List);
                Assert.NotEmpty(groups.List);
            }
        }

        public class TheGetInfoMethod : GroupClientTests
        {
            [Fact]
            public async Task GetInfo()
            {
                const int id = 99123;
                var group = await GroupClient.GetInfo(id);

                Assert.NotNull(group);
                Assert.Equal(group.Id, id);
            }
        }

        public class TheGetMemberMethod : GroupClientTests
        {
            [Fact]
            public async Task GetMember()
            {
                const int id = 99123;
                var members = await GroupClient.GetMembers(id);

                Assert.NotNull(members);
                Assert.NotEmpty(members.List);
            }
        }
    }
}
