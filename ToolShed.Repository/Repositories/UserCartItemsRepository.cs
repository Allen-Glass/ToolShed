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
    public class UserCartItemsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserCartItemsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddUserCartItemsAsync(UserCartItems userCartItems)
        {
            await toolShedContext.UserCartItemsSet
                .AddAsync(userCartItems);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddUserCartItemsAsync(IEnumerable<UserCartItems> userCartItems)
        {
            await toolShedContext.UserCartItemsSet
                .AddRangeAsync(userCartItems);
            await toolShedContext.SaveChangesAsync();
        }

        public int GetItemCountInCartAsync(Guid userId)
        {
            return toolShedContext.UserCartItemsSet
                .Where(c => c.UserId.Equals(userId))
                .Count();
        }

        public async Task<IEnumerable<UserCartItems>> GetUserCartItems(Guid userId)
        {
            return await toolShedContext.UserCartItemsSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync();
        }
    }
}
