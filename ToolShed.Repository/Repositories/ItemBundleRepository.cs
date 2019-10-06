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
    public class ItemBundleRepository
    {
        private readonly ToolShedContext toolShedContext;

        public ItemBundleRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddAsync(ItemBundle itemBundle)
        {
            if (itemBundle == null)
                throw new ArgumentNullException();

            await toolShedContext.ItemBundleSet
                .AddAsync(itemBundle);
            await toolShedContext.SaveChangesAsync();

            return itemBundle.ItemBundleId;
        }

        public async Task<ItemBundle> GetAsync(Guid itemBundleId)
        {
            if (itemBundleId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleSet
                .FirstOrDefaultAsync(c => c.ItemBundleId.Equals(itemBundleId));
        }

        public async Task<IEnumerable<ItemBundle>> ListAsync()
        {
            return await toolShedContext.ItemBundleSet
                .ToListAsync();
        }

        public async Task<IEnumerable<ItemBundle>> ListAsync(Guid tenantId)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemBundleSet
                .Where(c => c.TenantId.Equals(tenantId))
                .ToListAsync();
        }

        public async Task<IEnumerable<ItemBundle>> ListAsync(IEnumerable<Guid> itemBundleIds)
        {
            if (itemBundleIds == null)
                throw new ArgumentNullException();

            var itemBundleList = new List<ItemBundle>();
            foreach (var itemBundleId in itemBundleIds)
            {
                itemBundleList.Add(await GetAsync(itemBundleId));
            }

            return itemBundleList;
        }
    }
}
