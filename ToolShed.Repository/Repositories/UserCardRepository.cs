using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class UserCardRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserCardRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(UserCard userCard)
        {
            await toolShedContext.UserCardSet
                .AddAsync(userCard);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guid>> ListIdsAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            return await toolShedContext.UserCardSet
                .Where(c => c.UserId.Equals(userId))
                .Select(c => c.CardId)
                .ToListAsync();
        }

        public async Task DeleteAsync(UserCard userCard)
        {
            toolShedContext.UserCardSet
                .Remove(userCard);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
