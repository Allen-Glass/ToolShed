using System;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class CardSQLService : ICardSQLService
    {
        private readonly CardRepository cardRepository;

        public CardSQLService(CardRepository cardRepository)
        {
            this.cardRepository = cardRepository;
        }

         public async Task StoreCardInformation(Card card)
        {
            if (card == null)
                throw new ArgumentNullException();

            var cardId = await cardRepository.AddCardAsync(card);
        }
    }
}
