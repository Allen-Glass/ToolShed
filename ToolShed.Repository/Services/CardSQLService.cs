using System;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
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

            var cardId = await cardRepository.AddCardAsync(CardMapping.CreateDtoCard(card));
        }
    }
}
