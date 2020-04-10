using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task AddItemToDispenserAsync(Guid itemId, Guid dispenserId, CancellationToken cancellationToken = default)
        {
            var dispenserItem = new DispenserItem
            {
                ItemId = itemId,
                DispenserId = dispenserId
            };
            await toolShedContext.DispenserItemSet
                .AddAsync(dispenserItem);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> GetDispenserByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.DispenserItemSet
                .Where(c => c.ItemId.Equals(itemId))
                .Select(c => c.DispenserId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> GetAllItemsFromDispenserAsync(Guid dispenserId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.DispenserItemSet
                .Where(c => c.DispenserId.Equals(dispenserId))
                .Select(c => c.ItemId)
                .ToListAsync(cancellationToken);
        }

        public async Task RemoveItemFromDispensery(DispenserItem dispenserTool, CancellationToken cancellationToken = default)
        {
            toolShedContext.DispenserItemSet
                .Remove(dispenserTool);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
