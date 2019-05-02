using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class ItemRentalDetailsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public ItemRentalDetailsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddItemRentalDetailsAsync(ItemRentalDetails itemRentalDetails)
        {
            if (itemRentalDetails == null)
                throw new ArgumentNullException();

            await toolShedContext.ItemRentalDetailsSet
                .AddAsync(itemRentalDetails);
            await toolShedContext.SaveChangesAsync();
        }

        public virtual async Task<ItemRentalDetails> GetItemRentalDetailsAsync(Guid itemRentalDetailsId)
        {
            if (itemRentalDetailsId == Guid.Empty)
                throw new ArgumentNullException();

            var itemRentalDetails = await toolShedContext.ItemRentalDetailsSet
                .FirstOrDefaultAsync(c => c.ItemRentalDetailsId.Equals(itemRentalDetailsId));

            if (itemRentalDetails == null)
                throw new NullReferenceException();

            return itemRentalDetails;
        }
    }
}
