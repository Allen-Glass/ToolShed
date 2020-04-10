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
    public class CardRepository
    {
        private readonly ToolShedContext toolShedContext;

        public CardRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddAsync(Card card, CancellationToken cancellationToken = default)
        {
            await toolShedContext.CardSet
                .AddAsync(card);
            await toolShedContext.SaveChangesAsync(cancellationToken);

            return card.CardId;
        }

        public async Task<Card> GetAsync(Guid cardId, CancellationToken cancellationToken = default)
        {
            if (cardId == Guid.Empty)
                throw new ArgumentNullException();

            var card = await toolShedContext.CardSet
                .FirstOrDefaultAsync(c => c.CardId.Equals(cardId), cancellationToken);

            if (card == null)
                throw new NullReferenceException();

            return card;
        }

        public async Task<IEnumerable<Card>> ListAsync(IEnumerable<Guid> cardIds, CancellationToken cancellationToken = default)
        {
            if (cardIds == null)
                throw new ArgumentNullException();

            var cards = await toolShedContext.CardSet
                .Where(c => cardIds.Contains(c.CardId))
                .ToListAsync(cancellationToken);

            if (cards == null)
                throw new NullReferenceException();

            return cards;
        }

        public async Task<IEnumerable<Card>> GetCardsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            var cards = await toolShedContext.CardSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync(cancellationToken);

            if (cards == null)
                throw new NullReferenceException();

            return cards;
        }

        public async Task UpdateAsync(Card card, CancellationToken cancellationToken = default)
        {
            if (card == null)
                throw new ArgumentNullException();

            toolShedContext.CardSet
                .Update(card);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Card card, CancellationToken cancellationToken = default)
        {
            toolShedContext.CardSet
                .Remove(card);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid cardId, CancellationToken cancellationToken = default)
        {
            toolShedContext.CardSet
                .Remove(await GetAsync(cardId));
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
