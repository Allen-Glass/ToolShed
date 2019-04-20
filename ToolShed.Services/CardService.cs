using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Services;

namespace ToolShed.Services
{
    public class CardService
    {
        private readonly ICardSQLService cardSQLService;

        public CardService(ICardSQLService cardSQLService)
        {
            this.cardSQLService = cardSQLService;
        }

        public async Task AddCardAsync(Card card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            await cardSQLService.AddCardAsync(card);
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            return await cardSQLService.GetCardsAsync(userId);
        }
    }
}
