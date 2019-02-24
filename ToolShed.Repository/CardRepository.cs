using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.User;
using ToolShed.Repository.Context;

namespace ToolShed.Repository
{
    public class CardRepository
    {
        private readonly ToolShedContext toolShedContext;

        public CardRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddCardAsync(Card card)
        {
            await toolShedContext.CardSet.AddAsync(card);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Card>> GetCardByUserIdAsync(string userId)
        {
            return await toolShedContext.CardSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync();              
        }

        public async Task UpdateCardBillingAddress(Card card)
        {
            await toolShedContext.CardSet
                .Where(c => c.CardId.Equals(card.CardId))
                .FirstOrDefaultAsync(c => c.BillingAddress.Equals(card.BillingAddress));
        }

        public async Task DeleteCardAsync(string cardId)
        {

        }

        public async Task DeleteCardAsync(Card card)
        {
            toolShedContext.CardSet
                .Remove(card);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
