using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;

namespace ToolShed.Services
{
    public class CardService
    {
        private readonly ICardDataService cardSQLService;

        public CardService(ICardDataService cardSQLService)
        {
            this.cardSQLService = cardSQLService;
        }

        public async Task AddCardAsync(Card card, CancellationToken cancellationToken = default)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            await cardSQLService.AddCardAsync(card, cancellationToken);
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            return await cardSQLService.GetCardsAsync(userId, cancellationToken);
        }
    }
}
