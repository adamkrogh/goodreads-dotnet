using System.Threading.Tasks;
using Goodreads.Clients;
using Xunit;

namespace Goodreads.Tests
{
    public class GroupEndpointTests
    {
        private readonly IGroupsEndpoint GroupEndpoint;

        public GroupEndpointTests()
        {
            GroupEndpoint = Helper.GetAuthClient().Groups;
        }

        public class TheGetListByUserMethod : GroupEndpointTests
        {
            [Fact]
            public async Task GetListByUserToGroup()
            {
                var list = await GroupEndpoint.GetListByUser(690273);

                Assert.NotNull(list);
            }
        }

        public class TheGetGroupsMethod : GroupEndpointTests
        {
            [Fact]
            public async Task GetGroups()
            {
                var groups = await GroupEndpoint.GetGroups("English");

                Assert.NotNull(groups);
                Assert.NotNull(groups.List);
                Assert.NotEmpty(groups.List);
            }
        }

        public class TheGetInfoMethod : GroupEndpointTests
        {
            [Fact]
            public async Task GetInfo()
            {
                const int id = 99123;
                var group = await GroupEndpoint.GetInfo(id);

                Assert.NotNull(group);
                Assert.Equal(group.Id, id);
            }
        }

        public class TheGetMemberMethod : GroupEndpointTests
        {
            [Fact]
            public async Task GetMember()
            {
                const int id = 99123;
                var members = await GroupEndpoint.GetMembers(id);

                Assert.NotNull(members);
                Assert.NotEmpty(members.List);
            }
        }
    }
}
