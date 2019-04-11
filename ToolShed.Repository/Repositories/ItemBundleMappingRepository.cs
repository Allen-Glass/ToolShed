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

        public async Task AddItemBundleMappingAsync(ItemBundleMapping itemBundleMapping)
        {
            if (itemBundleMapping == null)
                throw new ArgumentNullException();

            await toolShedContext.ItemBundleMappingSet
                .AddAsync(itemBundleMapping);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<ItemBundleMapping> GetItemBundleMappingAsync(Guid itemBundleMappingId)
        {
            if (itemBundleMappingId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleMappingSet
                .FirstOrDefaultAsync(c => c.ItemBundleId.Equals(itemBundleMappingId));
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

        public async Task<Guid> GetItemBundleFromItem(Guid itemId)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleMappingSet
                .Where(c => c.ItemType.Equals(itemId))
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
