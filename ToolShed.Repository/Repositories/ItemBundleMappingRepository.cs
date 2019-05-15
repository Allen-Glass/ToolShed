using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class ItemBundleMappingRepository
    {
        private readonly ToolShedContext toolShedContext;

        public ItemBundleMappingRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddItemBundleMappingAsync(ItemBundleMapping itemBundleMapping)
        {
            if (itemBundleMapping == null)
                throw new ArgumentNullException();

            await toolShedContext.ItemBundleMappingSet
                .AddAsync(itemBundleMapping);
            await toolShedContext.SaveChangesAsync();

            return itemBundleMapping.ItemBundleMappingId;
        }

        public async Task<Guid> AddItemBundleMappingAsync(Guid itemId, Guid itemBundleId)
        {
            if (itemBundleId == Guid.Empty || itemId == Guid.Empty)
                throw new ArgumentNullException();

            var itemBundleMapping = new ItemBundleMapping
            {
                ItemBundleId = itemBundleId,
                ItemId = itemId
            };

            return await AddItemBundleMappingAsync(itemBundleMapping);
        }

        public async Task AddItemBundleMappingsAsync(IEnumerable<ItemBundleMapping> itemBundleMappings)
        {
            if (itemBundleMappings == null)
                throw new ArgumentNullException();

            await toolShedContext.ItemBundleMappingSet
                .AddRangeAsync(itemBundleMappings);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddItemBundleMappingsAsync(IEnumerable<Item> items, Guid itemBundleId)
        {
            if (itemBundleId == Guid.Empty || items == null)
                throw new ArgumentNullException();

            foreach(var item in items)
            {
                var itemBundleMapping = new ItemBundleMapping
                {
                    ItemBundleId = itemBundleId,
                    ItemId = item.ItemId
                };
                await toolShedContext.ItemBundleMappingSet
                    .AddAsync(itemBundleMapping);
            }

            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ItemBundleMapping>> GetItemBundleMappingAsync()
        {
            return await toolShedContext.ItemBundleMappingSet
                .ToListAsync();
        }

        public async Task<ItemBundleMapping> GetItemBundleMappingAsync(Guid itemBundleMappingId)
        {
            if (itemBundleMappingId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleMappingSet
                .FirstOrDefaultAsync(c => c.ItemBundleMappingId.Equals(itemBundleMappingId));
        }

        public async Task<IEnumerable<Guid>> GetAllItemIdsInBundle(Guid itemBundleId)
        {
            if (itemBundleId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleMappingSet
                .Where(c => c.ItemBundleId.Equals(itemBundleId))
                .Select(c => c.ItemBundleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetAllItemIdsInBundle()
        {
            return await toolShedContext.ItemBundleMappingSet
                .Select(c => c.ItemBundleId)
                .ToListAsync();
        }

        public async Task<Guid> GetItemBundleIdFromItemId(Guid itemId)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleMappingSet
                .Where(c => c.ItemId.Equals(itemId))
                .Select(c => c.ItemBundleId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ItemBundleMapping>> GetItemBundlesMappingAsync()
        {
            return await toolShedContext.ItemBundleMappingSet
                .ToListAsync();
        }
    }
}
