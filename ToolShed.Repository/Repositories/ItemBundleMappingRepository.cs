using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task<Guid> AddItemBundleMappingAsync(ItemBundleMapping itemBundleMapping, CancellationToken cancellationToken = default)
        {
            if (itemBundleMapping == null)
                throw new ArgumentNullException();

            await toolShedContext.ItemBundleMappingSet
                .AddAsync(itemBundleMapping);
            await toolShedContext.SaveChangesAsync(cancellationToken);

            return itemBundleMapping.ItemBundleMappingId;
        }

        public async Task<Guid> AddItemBundleMappingAsync(Guid itemId, Guid itemBundleId, CancellationToken cancellationToken = default)
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

        public async Task AddItemBundleMappingsAsync(IEnumerable<ItemBundleMapping> itemBundleMappings, CancellationToken cancellationToken = default)
        {
            if (itemBundleMappings == null)
                throw new ArgumentNullException();

            await toolShedContext.ItemBundleMappingSet
                .AddRangeAsync(itemBundleMappings);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddItemBundleMappingsAsync(IEnumerable<Item> items, Guid itemBundleId, CancellationToken cancellationToken = default)
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
                    .AddAsync(itemBundleMapping, cancellationToken);
            }

            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ItemBundleMapping>> GetItemBundleMappingAsync(CancellationToken cancellationToken = default)
        {
            return await toolShedContext.ItemBundleMappingSet
                .ToListAsync(cancellationToken);
        }

        public async Task<ItemBundleMapping> GetItemBundleMappingAsync(Guid itemBundleMappingId, CancellationToken cancellationToken = default)
        {
            if (itemBundleMappingId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleMappingSet
                .FirstOrDefaultAsync(c => c.ItemBundleMappingId.Equals(itemBundleMappingId), cancellationToken);
        }

        public async Task<IEnumerable<Guid>> GetAllItemIdsInBundle(Guid itemBundleId, CancellationToken cancellationToken = default)
        {
            if (itemBundleId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleMappingSet
                .Where(c => c.ItemBundleId.Equals(itemBundleId))
                .Select(c => c.ItemBundleId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> GetAllItemIdsInBundle(CancellationToken cancellationToken = default)
        {
            return await toolShedContext.ItemBundleMappingSet
                .Select(c => c.ItemBundleId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Guid> GetItemBundleIdFromItemId(Guid itemId, CancellationToken cancellationToken = default)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleMappingSet
                .Where(c => c.ItemId.Equals(itemId))
                .Select(c => c.ItemBundleId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<ItemBundleMapping>> GetItemBundlesMappingAsync(CancellationToken cancellationToken = default)
        {
            return await toolShedContext.ItemBundleMappingSet
                .ToListAsync(cancellationToken);
        }
    }
}
