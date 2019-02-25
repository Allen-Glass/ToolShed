using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Toolshed.Models.SQL;
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

        public async Task AddDispenserAsync(Dispenser dispenser)
        {
            await toolShedContext.DispenserSet
                .AddAsync(dispenser);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dispenser>> GetAllDispensers()
        {
            return await toolShedContext.DispenserSet
                .ToListAsync();
        }

        public async Task<Dispenser> GetDispenserByDispenserId(Guid dispenserId)
        {
            return await toolShedContext.DispenserSet
                .FirstOrDefaultAsync(c => c.DispenserId.Equals(dispenserId));
        }

        public async Task RemoveDispenserAsync(Dispenser dispenser)
        {
            toolShedContext.DispenserSet
                .Remove(dispenser);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
