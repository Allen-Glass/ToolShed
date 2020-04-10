using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class DispenserRepository
    {
        private readonly ToolShedContext toolShedContext;

        public DispenserRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddAsync(Dispenser dispenser, CancellationToken cancellationToken = default)
        {
            await toolShedContext.DispenserSet
                .AddAsync(dispenser);
            await toolShedContext.SaveChangesAsync(cancellationToken);

            return dispenser.DispenserId;
        }

        public async Task<IEnumerable<Dispenser>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await toolShedContext.DispenserSet
                .ToListAsync(cancellationToken);
        }

        public async Task<Dispenser> GetDispenserByDispenserIdAsync(Guid dispenserId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.DispenserSet
                .FirstOrDefaultAsync(c => c.DispenserId.Equals(dispenserId), cancellationToken);
        }

        public async Task<Guid> GetDispenserAddressIdAsync(Guid dispenserId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.DispenserSet
                .Where(c => c.DispenserId.Equals(dispenserId))
                .Select(c => c.DispenserAddressId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<string> GetDispenserIotNameAsync(Guid dispenserId, CancellationToken cancellationToken = default)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var dispenserIotName = await toolShedContext.DispenserSet
                .Where(c => c.DispenserId.Equals(dispenserId))
                .Select(c => c.DispenserIotName)
                .FirstOrDefaultAsync(cancellationToken);

            if (string.IsNullOrEmpty(dispenserIotName))
                throw new NullReferenceException();

            return dispenserIotName;
        }

        public async Task DeleteAsync(Dispenser dispenser, CancellationToken cancellationToken = default)
        {
            toolShedContext.DispenserSet
                .Remove(dispenser);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
