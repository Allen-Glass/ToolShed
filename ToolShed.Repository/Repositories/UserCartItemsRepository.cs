using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddAsync(UserCartItems userCartItems)
        {
            await toolShedContext.UserCartItemsSet
                .AddAsync(userCartItems);
            await toolShedContext.SaveChangesAsync();
        }     

        public async Task AddAsync(IEnumerable<UserCartItems> userCartItems)
        {
            await toolShedContext.UserCartItemsSet
                .AddRangeAsync(userCartItems);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddAsync(Guid userCartId, Guid itemId)
        {
            var userCartItems = new UserCartItems
            {
                UserCartId = userCartId,
                ItemId = itemId
            };
            await toolShedContext.UserCartItemsSet
                .AddAsync(userCartItems);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddAsync(Guid userCartId, IEnumerable<Guid> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                var userCartItems = new UserCartItems
                {
                    UserCartId = userCartId,
                    ItemId = itemId
                };
                await toolShedContext.UserCartItemsSet
                    .AddAsync(userCartItems);
                await toolShedContext.SaveChangesAsync();
            }   
        }

        public int GetCountAsync(Guid userCartId)
        {
            return toolShedContext.UserCartItemsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .Count();
        }

        public async Task<IEnumerable<UserCartItems>> ListAsync(Guid userCartId)
        {
            return await toolShedContext.UserCartItemsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Guid>> ListIdsAsync(Guid userCartId)
        {
            return await toolShedContext.UserCartItemsSet
                .Where(c => c.UserCartId.Equals(userCartId))
                .Select(c => c.ItemId)
                .ToListAsync();
        }

        public async Task DeleteUserCartItemsAsync(Guid userCartId)
        {
            var userCartItems = await ListAsync(userCartId);
            foreach (var item in userCartItems)
            {
                toolShedContext.UserCartItemsSet
                    .Remove(item);
            }

            await toolShedContext.SaveChangesAsync();
        }
    }
}
