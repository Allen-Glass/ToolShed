using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<Guid> AddItemAsync(Item item)
        {
            await toolShedContext.ItemSet
                .AddAsync(item);
            await toolShedContext.SaveChangesAsync();

            return item.ItemId;
        }

        public async Task<Item> GetItemByItemIdAsync(Guid itemId)
        {
            return await toolShedContext.ItemSet
                .FirstOrDefaultAsync(c => c.ItemId.Equals(itemId));
        }

        public async Task<IEnumerable<Item>> GetItemsByItemIdsAsync(IEnumerable<Guid> itemIds)
        {
            var itemsList = new List<Item>();
            foreach (var id in itemIds)
            {
                var item = await GetItemByItemIdAsync(id);
                itemsList.Add(item);
            }

            return itemsList;
        }

        public async Task DeleteItemAsync(Item item)
        {
            toolShedContext.ItemSet
                .Remove(item);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
