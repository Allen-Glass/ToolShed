using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
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

        public async Task<Card> GetCardAsync(Guid cardId)
        {
            if (cardId == Guid.Empty)
                throw new ArgumentNullException();

            var card = await toolShedContext.CardSet
                .FirstOrDefaultAsync(c => c.CardId.Equals(cardId));

            if (card == null)
                throw new NullReferenceException();

            return card;
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(IEnumerable<Guid> cardIds)
        {
            if (cardIds == null)
                throw new ArgumentNullException();

            var cards = await toolShedContext.CardSet
                .Where(c => cardIds.Contains(c.CardId))
                .ToListAsync();

            if (cards == null)
                throw new NullReferenceException();

            return cards;
        }

        public async Task<IEnumerable<Card>> GetCardsByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            var cards = await toolShedContext.CardSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync();

            if (cards == null)
                throw new NullReferenceException();

            return cards;
        }

        public async Task UpdateCardAsync(Card card)
        {
            if (card == null)
                throw new ArgumentNullException();

            toolShedContext.CardSet
                .Update(card);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteCardAsync(Card card)
        {
            toolShedContext.CardSet
                .Remove(card);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteCardAsync(Guid cardId)
        {
            toolShedContext.CardSet
                .Remove(await GetCardAsync(cardId));
            await toolShedContext.SaveChangesAsync();
        }
    }
}
