using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class UserCartRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserCartRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(UserCart userCart)
        {
            await toolShedContext.UserCartSet
                .AddAsync(userCart);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<bool> DoesUserHaveItemsInCart(Guid userId)
        {
            return await toolShedContext.UserCartSet
                .AnyAsync(c => c.UserId.Equals(userId));
        }

        public async Task<UserCart> GetAsync(Guid userId)
        {
            return await toolShedContext.UserCartSet
                .FirstOrDefaultAsync(c => c.UserId.Equals(userId));
        }

        public async Task<Guid> GetUserCartIdAsync(Guid userId)
        {
            var userCart = await toolShedContext.UserCartSet
                .FirstOrDefaultAsync(c => c.UserId.Equals(userId));

            return userCart.UserCartId;
        }

        public async Task DeleteAsync(Guid userId)
        {
            var userCart = await GetAsync(userId);
            toolShedContext.UserCartSet
                .Remove(userCart);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
