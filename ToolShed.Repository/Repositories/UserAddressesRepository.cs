using System.Threading.Tasks;
using ToolShed.Repository.Context;
using ToolShed.Models.Repository;

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
