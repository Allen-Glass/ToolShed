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
    public class UserCartItemRentalsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserCartItemRentalsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(UserCartItemRentals userCartItemRentals, CancellationToken cancellationToken = default)
        {
            await toolShedContext.UserCartItemRentalsSet
                .AddAsync(userCartItemRentals, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAsync(IEnumerable<UserCartItemRentals> userCartItemRentals, CancellationToken cancellationToken = default)
        {
            await toolShedContext.UserCartItemRentalsSet
                .AddRangeAsync(userCartItemRentals, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAsync(Guid userCartId, Guid itemRentalDetailsId, CancellationToken cancellationToken = default)
        {
            var userCartItemRental = new UserCartItemRentals
            {
                UserCartId = userCartId,
                ItemRentalDetailsId = itemRentalDetailsId
            };
            await toolShedContext.UserCartItemRentalsSet
                .AddRangeAsync(userCartItemRental);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAsync(Guid userCartId, IEnumerable<Guid> itemRentalDetailsIds, CancellationToken cancellationToken = default)
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

        public Task<int> GetItemCountInCartAsync(Guid userCartId, CancellationToken cancellationToken = default)
        {
            return toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> ListAsync(UserCartItemRentals userCartItemRentals, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartItemRentals.UserCartId))
                .Select(c => c.ItemRentalDetailsId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> ListIdsAsync(Guid userCartId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .Select(c => c.ItemRentalDetailsId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserCartItemRentals>> GetUserCartItemRentals(Guid userCartId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserCartItemRentalsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid userCartId, CancellationToken cancellationToken = default)
        {
            var userCartItemRentals = await GetUserCartItemRentals(userCartId);
            foreach (var item in userCartItemRentals)
            {
                toolShedContext.UserCartItemRentalsSet
                    .Remove(item);
            }

            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
