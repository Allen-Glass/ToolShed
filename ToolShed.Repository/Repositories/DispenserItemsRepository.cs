using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class DispenserItemsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public DispenserItemsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddItemToDispenserAsync(Guid itemId, Guid dispenserId)
        {
            var dispenserItem = new DispenserItem
            {
                ItemId = itemId,
                DispenserId = dispenserId
            };
            await toolShedContext.DispenserItemSet
                .AddAsync(dispenserItem);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<Guid> GetDispenserByItemIdAsync(Guid itemId)
        {
            return await toolShedContext.DispenserItemSet
                .Where(c => c.ItemId.Equals(itemId))
                .Select(c => c.DispenserId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Guid>> GetAllItemsFromDispenseryAsync(Guid dispenserId)
        {
            return await toolShedContext.DispenserItemSet
                .Where(c => c.DispenserId.Equals(dispenserId))
                .Select(c => c.ItemId)
                .ToListAsync();
        }

        public async Task RemoveItemFromDispensery(DispenserItem dispenserTool)
        {
            toolShedContext.DispenserItemSet
                .Remove(dispenserTool);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
