using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class ItemTypeRepository
    {
        private readonly ToolShedContext toolShedContext;

        public ItemTypeRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddAsync(ItemType itemType, CancellationToken cancellationToken = default)
        {
            if (itemType == null)
                throw new ArgumentNullException();

            await toolShedContext.AddAsync(itemType);
            await toolShedContext.SaveChangesAsync(cancellationToken);

            return itemType.ItemTypeId;
        }

        public async Task<ItemType> GetAsync(Guid itemTypeId, CancellationToken cancellationToken = default)
        {
            if (itemTypeId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemTypeSet
                .FirstOrDefaultAsync(c => c.ItemTypeId.Equals(itemTypeId), cancellationToken);
        }

        public async Task DeleteAsync(ItemType itemType, CancellationToken cancellationToken = default)
        {
            toolShedContext.Remove(itemType);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
