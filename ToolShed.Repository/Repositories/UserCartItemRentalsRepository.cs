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

        public async Task AddAsync(UserCartItemRentals userCartItemRentals)
        {
            await toolShedContext.UserCartItemRentalsSet
                .AddAsync(userCartItemRentals);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<UserCartItemRentals> userCartItemRentals)
        {
            await toolShedContext.UserCartItemRentalsSet
                .AddRangeAsync(userCartItemRentals);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddAsync(Guid userCartId, Guid itemRentalDetailsId)
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

        public async Task AddAsync(Guid userCartId, IEnumerable<Guid> itemRentalDetailsIds)
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

        public async Task<IEnumerable<Guid>> ListAsync(UserCartItemRentals userCartItemRentals)
        {
            return await toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartItemRentals.UserCartId))
                .Select(c => c.ItemRentalDetailsId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Guid>> ListIdsAsync(Guid userCartId)
        {
            return await toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .Select(c => c.ItemRentalDetailsId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserCartItemRentals>> GetUserCartItemRentals(Guid userCartId)
        {
            return await toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .ToListAsync();
        }

        public async Task DeleteAsync(Guid userCartId)
        {
            var userCartItemRentals = await GetUserCartItemRentals(userCartId);
            foreach (var item in userCartItemRentals)
            {
                toolShedContext.UserCartItemRentalsSet
                    .Remove(item);
            }

            await toolShedContext.SaveChangesAsync();
        }
    }
}
