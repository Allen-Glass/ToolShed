﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddDispenserAsync(Dispenser dispenser)
        {
            await toolShedContext.DispenserSet
                .AddAsync(dispenser);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dispenser>> GetAllDispensersAsync()
        {
            return await toolShedContext.DispenserSet
                .ToListAsync();
        }

        public async Task<Dispenser> GetDispenserByDispenserIdAsync(Guid dispenserId)
        {
            return await toolShedContext.DispenserSet
                .FirstOrDefaultAsync(c => c.DispenserId.Equals(dispenserId));
        }

        public async Task<Guid> GetDispenserAddressIdAsync(Guid dispenserId)
        {
            return await toolShedContext.DispenserSet
                .Where(c => c.DispenserId.Equals(dispenserId))
                .Select(c => c.DispenserAddressId)
                .FirstOrDefaultAsync();
        }

        public async Task<string> GetDispenserIotNameAsync(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var dispenserIotName = await toolShedContext.DispenserSet
                .Where(c => c.DispenserId.Equals(dispenserId))
                .Select(c => c.DispenserIotId)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(dispenserIotName))
                throw new NullReferenceException();

            return dispenserIotName;
        }

        public async Task RemoveDispenserAsync(Dispenser dispenser)
        {
            toolShedContext.DispenserSet
                .Remove(dispenser);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
