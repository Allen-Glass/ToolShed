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
    public class ItemRepository
    {
        private readonly ToolShedContext toolShedContext;

        public ItemRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddAsync(Item item, CancellationToken cancellationToken = default)
        {
            await toolShedContext.ItemSet
                .AddAsync(item);
            await toolShedContext.SaveChangesAsync(cancellationToken);

            return item.ItemId;
        }

        public async Task<Item> GetAsync(Guid itemId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.ItemSet
                .FirstOrDefaultAsync(c => c.ItemId.Equals(itemId), cancellationToken);
        }

        public async Task<IEnumerable<Item>> ListAsync(IEnumerable<Guid> itemIds, CancellationToken cancellationToken = default)
        {
            var itemsList = new List<Item>();
            foreach (var id in itemIds)
            {
                var item = await GetAsync(id);
                itemsList.Add(item);
            }

            return itemsList;
        }

        public async Task DeleteAsync(Item item, CancellationToken cancellationToken = default)
        {
            toolShedContext.ItemSet
                .Remove(item);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
