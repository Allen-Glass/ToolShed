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
    public class UserCardRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserCardRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(UserCard userCard, CancellationToken cancellationToken = default)
        {
            await toolShedContext.UserCardSet
                .AddAsync(userCard);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> ListIdsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            return await toolShedContext.UserCardSet
                .Where(c => c.UserId.Equals(userId))
                .Select(c => c.CardId)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(UserCard userCard, CancellationToken cancellationToken = default)
        {
            toolShedContext.UserCardSet
                .Remove(userCard);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
