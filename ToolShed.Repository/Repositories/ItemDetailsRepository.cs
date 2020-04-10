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
    public class ItemDetailsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public ItemDetailsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(ItemDetails itemDetails, CancellationToken cancellationToken = default)
        {
            if (itemDetails == null)
                throw new ArgumentNullException();

            await toolShedContext.ItemDetailsSet
                .AddAsync(itemDetails);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ItemDetails>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await toolShedContext.ItemDetailsSet
                .ToListAsync(cancellationToken);
        }

        public async Task<ItemDetails> GetAsync(Guid itemDetailsId, CancellationToken cancellationToken = default)
        {
            if (itemDetailsId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.ItemDetailsSet
                .FirstOrDefaultAsync(c => c.ItemDetailsId.Equals(itemDetailsId), cancellationToken);
        }

        public async Task<IEnumerable<ItemDetails>> ListAsync(IEnumerable<Guid> itemDetailsId, CancellationToken cancellationToken = default)
        {
            if (itemDetailsId == null)
                throw new ArgumentNullException();

            var itemDetailList = new List<ItemDetails>();
            foreach (var itemDetailId in itemDetailsId)
            {
                itemDetailList.Add(await GetAsync(itemDetailId, cancellationToken));
            }

            return itemDetailList;
        }
    }
}
