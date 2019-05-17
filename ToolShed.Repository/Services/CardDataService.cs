using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class CardDataService : ICardDataService
    {
        private readonly CardRepository cardRepository;
        private readonly UserCardRepository userCardRepository;
        private readonly CardAddressRepository cardAddressRepository;
        private readonly AddressRepository addressRepository;

        public CardDataService(CardRepository cardRepository
            , UserCardRepository userCardRepository
            , CardAddressRepository cardAddressRepository
            , AddressRepository addressRepository)
        {
            this.cardRepository = cardRepository;
            this.userCardRepository = userCardRepository;
            this.cardAddressRepository = cardAddressRepository;
            this.addressRepository = addressRepository;
        }

         public async Task AddCardAsync(Card card)
        {
            if (card == null || card.UserId == Guid.Empty)
                throw new ArgumentNullException();

            await cardRepository.AddCardAsync(CardMapping.CreateDtoCard(card));
            await userCardRepository.AddUserCardAsync(CardMapping.CreateUserCardDTO(card));
            await addressRepository.AddAddressAsync(AddressMapping.CreateDtoAddress(card.BillingAddress));
        }

        public async Task<Card> GetCardAsync(Guid cardId)
        {
            if (cardId == Guid.Empty)
                throw new ArgumentNullException(nameof(cardId));

            var card = await cardRepository.GetCardAsync(cardId);
            return CardMapping.ConvertDtoCardToCard(card);
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoCards = await cardRepository.GetCardsByUserIdAsync(userId);
            var cards = await MapAddressesToCardsAsync(dtoCards);
            return cards;
        }

        public async Task DeleteCardsAsync(IEnumerable<Card> cards)
        {
            if (cards == null)
                throw new ArgumentNullException();

            foreach (var card in cards)
            {
                await cardRepository.DeleteCardAsync(CardMapping.CreateDtoCard(card));
            }
        }

        private async Task<IEnumerable<Card>> MapAddressesToCardsAsync(IEnumerable<Models.Repository.Card> cards)
        {
            var cardList = new List<Card>();
            foreach (var dtoCard in cards)
            {
                var address = await addressRepository.GetAddressAsync(dtoCard.AddressId);
                var card = CardMapping.ConvertDtoCardToCard(dtoCard);
                card.BillingAddress = AddressMapping.ConvertDtoAddressToAddress(address);
                cardList.Add(card);
            }

            return cardList;
        }
    }
}
