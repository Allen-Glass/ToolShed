using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class UserCartItemsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserCartItemsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(UserCartItems userCartItems, CancellationToken cancellationToken = default)
        {
            await toolShedContext.UserCartItemsSet
                .AddAsync(userCartItems);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }     

        public async Task AddAsync(IEnumerable<UserCartItems> userCartItems, CancellationToken cancellationToken = default)
        {
            await toolShedContext.UserCartItemsSet
                .AddRangeAsync(userCartItems);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAsync(Guid userCartId, Guid itemId, CancellationToken cancellationToken = default)
        {
            var userCartItems = new UserCartItems
            {
                UserCartId = userCartId,
                ItemId = itemId
            };
            await toolShedContext.UserCartItemsSet
                .AddAsync(userCartItems, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAsync(Guid userCartId, IEnumerable<Guid> itemIds, CancellationToken cancellationToken = default)
        {
            foreach (var itemId in itemIds)
            {
                var userCartItems = new UserCartItems
                {
                    UserCartId = userCartId,
                    ItemId = itemId
                };
                await toolShedContext.UserCartItemsSet
                    .AddAsync(userCartItems, cancellationToken);
                await toolShedContext.SaveChangesAsync(cancellationToken);
            }   
        }

        public Task<int> GetCountAsync(Guid userCartId, CancellationToken cancellationToken = default)
        {
            return toolShedContext.UserCartItemsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserCartItems>> ListAsync(Guid userCartId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserCartItemsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> ListIdsAsync(Guid userCartId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserCartItemsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .Select(c => c.ItemId)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteUserCartItemsAsync(Guid userCartId, CancellationToken cancellationToken = default)
        {
            var userCartItems = await ListAsync(userCartId);
            foreach (var item in userCartItems)
            {
                toolShedContext.UserCartItemsSet
                    .Remove(item);
            }

            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
