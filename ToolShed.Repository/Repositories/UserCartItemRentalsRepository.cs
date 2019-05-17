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
    public class UserCartItemRentalsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserCartItemRentalsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddUserRentalItemAsync(UserCartItemRentals userCartItemRentals)
        {
            await toolShedContext.UserCartItemRentalsSet
                .AddAsync(userCartItemRentals);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddUserRentalItemAsync(IEnumerable<UserCartItemRentals> userCartItemRentals)
        {
            await toolShedContext.UserCartItemRentalsSet
                .AddRangeAsync(userCartItemRentals);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddUserRentalItemAsync(Guid userCartId, Guid itemRentalDetailsId)
        {
            var userCartItemRental = new UserCartItemRentals
            {
                UserCartId = userCartId,
                ItemRentalDetailsId = itemRentalDetailsId
            };
            await toolShedContext.UserCartItemRentalsSet
                .AddRangeAsync(userCartItemRental);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddUserRentalItemAsync(Guid userCartId, IEnumerable<Guid> itemRentalDetailsIds)
        {
            foreach (var itemRentalDetailsId in itemRentalDetailsIds)
            {
                var userCartItemRental = new UserCartItemRentals
                {
                    UserCartId = userCartId,
                    ItemRentalDetailsId = itemRentalDetailsId
                };
                await toolShedContext.UserCartItemRentalsSet
                    .AddRangeAsync(userCartItemRental);
            }
            await toolShedContext.SaveChangesAsync();
        }

        public int GetItemCountInCartAsync(Guid userCartId)
        {
            return toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .Count();
        }

        public async Task<IEnumerable<Guid>> GetItemRentalIdsAsync(UserCartItemRentals userCartItemRentals)
        {
            return await toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartItemRentals.UserCartId))
                .Select(c => c.ItemRentalDetailsId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetItemRentalIdsAsync(Guid userCartId)
        {
            return await toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .Select(c => c.ItemRentalDetailsId)
                .ToListAsync();
        }
    }
}
