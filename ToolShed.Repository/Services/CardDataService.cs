using System;
using System.Collections.Generic;
using System.Threading;
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

        public CardDataService(CardRepository cardRepository,
            UserCardRepository userCardRepository,
            CardAddressRepository cardAddressRepository,
            AddressRepository addressRepository)
        {
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            this.userCardRepository = userCardRepository ?? throw new ArgumentNullException(nameof(userCardRepository));
            this.cardAddressRepository = cardAddressRepository ?? throw new ArgumentNullException(nameof(cardAddressRepository));
            this.addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

         public async Task AddCardAsync(Card card, CancellationToken cancellationToken = default)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            if (card.UserId == Guid.Empty)
                throw new ArgumentNullException(nameof(card.UserId));

            await cardRepository.AddAsync(card.CreateDtoCard());
            await userCardRepository.AddAsync(card.CreateUserCardDTO());
            await addressRepository.AddAsync(card.BillingAddress.CreateDtoAddress());
        }

        public async Task<Card> GetCardAsync(Guid cardId, CancellationToken cancellationToken = default)
        {
            if (cardId == Guid.Empty)
                throw new ArgumentNullException(nameof(cardId));

            var card = await cardRepository.GetAsync(cardId);
            return card.ConvertDtoCardToCard();
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            var dtoCards = await cardRepository.GetCardsByUserIdAsync(userId);
            var cards = await MapAddressesToCardsAsync(dtoCards);
            return cards;
        }

        public async Task DeleteCardsAsync(IEnumerable<Card> cards, CancellationToken cancellationToken = default)
        {
            if (cards == null)
                throw new ArgumentNullException(nameof(cards));

            foreach (var card in cards)
            {
                await cardRepository.DeleteAsync(card.CreateDtoCard());
            }
        }

        private async Task<IEnumerable<Card>> MapAddressesToCardsAsync(IEnumerable<Models.Repository.Card> cards, CancellationToken cancellationToken = default)
        {
            if (cards == null)
                throw new ArgumentNullException(nameof(cards));

            var cardList = new List<Card>();
            foreach (var dtoCard in cards)
            {
                var address = await addressRepository.GetAsync(dtoCard.AddressId, cancellationToken);
                var card = dtoCard.ConvertDtoCardToCard();
                card.BillingAddress = address.ConvertDtoAddressToAddress();
                cardList.Add(card);
            }

            return cardList;
        }
    }
}
