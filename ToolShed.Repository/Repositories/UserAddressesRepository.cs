using System.Threading.Tasks;
using Toolshed.Repository.Models;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class UserAddressesRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserAddressesRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddUserAddressAsync(UserAddresses userAddresses)
        {
            await toolShedContext.UserAddressesSet
                .AddAsync(userAddresses);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteCardAsync(UserAddresses userAddresses)
        {
            toolShedContext.UserAddressesSet
                .Remove(userAddresses);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
