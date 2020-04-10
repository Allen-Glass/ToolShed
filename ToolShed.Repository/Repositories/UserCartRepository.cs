using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
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

        public async Task AddAsync(UserCart userCart, CancellationToken cancellationToken = default)
        {
            await toolShedContext.UserCartSet
                .AddAsync(userCart);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DoesUserHaveItemsInCart(Guid userId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserCartSet
                .AnyAsync(c => c.UserId.Equals(userId), cancellationToken);
        }

        public async Task<UserCart> GetAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserCartSet
                .FirstOrDefaultAsync(c => c.UserId.Equals(userId), cancellationToken);
        }

        public async Task<Guid> GetUserCartIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var userCart = await toolShedContext.UserCartSet
                .FirstOrDefaultAsync(c => c.UserId.Equals(userId), cancellationToken);

            return userCart.UserCartId;
        }

        public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var userCart = await GetAsync(userId);
            toolShedContext.UserCartSet
                .Remove(userCart);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
