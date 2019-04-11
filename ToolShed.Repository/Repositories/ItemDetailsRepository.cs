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
    public class ItemDetailsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public ItemDetailsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddItemDetailsAsync(ItemDetails itemDetails)
        {
            if (itemDetails == null)
                throw new ArgumentNullException();

            await toolShedContext.ItemDetailsSet
                .AddAsync(itemDetails);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ItemDetails>> GetItemBundlesAsync()
        {
            return await toolShedContext.ItemDetailsSet
                .ToListAsync();
        }

        public async Task<ItemDetails> GetItemDetailsAsync(Guid itemDetailsId)
        {
            if (itemDetailsId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemDetailsSet
                .FirstOrDefaultAsync(c => c.ItemDetailsId.Equals(itemDetailsId));
        }

        public async Task<IEnumerable<ItemDetails>> GetItemDetailsAsync(IEnumerable<Guid> itemDetailsId)
        {
            if (itemDetailsId == null)
                throw new ArgumentNullException();

            var itemDetailList = new List<ItemDetails>();
            foreach (var itemDetailId in itemDetailsId)
            {
                itemDetailList.Add(await GetItemDetailsAsync(itemDetailId));
            }

            return itemDetailList;
        }
    }
}
