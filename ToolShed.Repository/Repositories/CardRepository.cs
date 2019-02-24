using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolshed.Models.SQL;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class CardRepository
    {
        private readonly ToolShedContext toolShedContext;

        public CardRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddCardAsync(Card card)
        {
            await toolShedContext.CardSet
                .AddAsync(card);
            await toolShedContext.SaveChangesAsync();

            return card.CardId;
        }

        public async Task<IEnumerable<Card>> GetCardByUserIdAsync(string userId)
        {
            return await toolShedContext.CardSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync();              
        }

        public async Task DeleteCardAsync(Card card)
        {
            toolShedContext.CardSet
                .Remove(card);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
