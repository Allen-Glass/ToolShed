using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolshed.Models.SQL;
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

        public async Task AddUserCardAsync(UserCard userCard)
        {
            await toolShedContext.UserCardSet
                .AddAsync(userCard);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteCardAsync(UserCard userCard)
        {
            toolShedContext.UserCardSet
                .Remove(userCard);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
