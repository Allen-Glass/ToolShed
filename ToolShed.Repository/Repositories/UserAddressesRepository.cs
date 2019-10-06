using System.Threading.Tasks;
using ToolShed.Repository.Context;
using ToolShed.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ToolShed.Repository.Repositories
{
    public class UserAddressesRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserAddressesRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(UserAddresses userAddresses)
        {
            await toolShedContext.UserAddressesSet
                .AddAsync(userAddresses);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guid>> ListIdsAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.UserAddressesSet
                .Where(c => c.UserId.Equals(userId))
                .Select(c => c.AddressId)
                .ToListAsync();
        }

        public async Task DeleteAsync(UserAddresses userAddresses)
        {
            toolShedContext.UserAddressesSet
                .Remove(userAddresses);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
