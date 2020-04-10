using System.Threading.Tasks;
using ToolShed.Repository.Context;
using ToolShed.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ToolShed.Repository.Repositories
{
    public class UserAddressesRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserAddressesRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(UserAddresses userAddresses, CancellationToken cancellationToken = default)
        {
            await toolShedContext.UserAddressesSet
                .AddAsync(userAddresses);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> ListIdsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.UserAddressesSet
                .Where(c => c.UserId.Equals(userId))
                .Select(c => c.AddressId)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(UserAddresses userAddresses, CancellationToken cancellationToken = default)
        {
            toolShedContext.UserAddressesSet
                .Remove(userAddresses);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
