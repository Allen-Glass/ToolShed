using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly UserRepository userRepository;
        private readonly AddressRepository addressRepository;
        private readonly CardRepository cardRepository;
        private readonly UserCardRepository userCardRepository;
        private readonly UserAddressesRepository userAddressesRepository;
        private readonly CardAddressRepository cardAddressRepository;

        public UserDataService(UserRepository userRepository
            , AddressRepository addressRepository
            , CardRepository cardRepository
            , UserCardRepository userCardRepository
            , UserAddressesRepository userAddressesRepository
            , CardAddressRepository cardAddressRepository)
        {
            this.userRepository = userRepository;
            this.addressRepository = addressRepository;
            this.cardRepository = cardRepository;
            this.userCardRepository = userCardRepository;
            this.userAddressesRepository = userAddressesRepository;
            this.cardAddressRepository = cardAddressRepository;
        }

        public async Task AddUserInformationAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userId = await userRepository.AddAsync(UserMapping.CreateDtoUser(user));
            if (user.Address != null)
            {
                var addressId = await addressRepository.AddAsync(AddressMapping.CreateDtoAddress(user.Address));
                await userAddressesRepository.AddAsync(AddressMapping.CreateUserAddressDTO(userId, addressId));
            }
        }

        public async Task CreateNewUserAccountAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await userRepository.AddAsync(UserMapping.CreateDtoUser(user));
        }

        public async Task<string> GetHashedPasswordAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            var hashedPassword = await userRepository.GetHashedPasswordAsync(userId);

            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentNullException(nameof(hashedPassword));

            return hashedPassword;
        }

        public async Task<User> GetUserInformationAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            IEnumerable<Models.Repository.Card> dtoCards;
            IEnumerable<Card> cards;
            IEnumerable<Models.Repository.Address> dtoAddresses;
            var dtoUser = await userRepository.GetAsync(userId);
            var user = UserMapping.ConvertDtoUser(dtoUser);
            var cardIds = await userCardRepository.ListIdsAsync(dtoUser.UserId);
            var addressIds = await userAddressesRepository.ListIdsAsync(dtoUser.UserId);

            if (cardIds != null)
            {
                dtoCards = await cardRepository.ListAsync(cardIds);
                cards = await MapAddressesToCardsAsync(dtoCards);
                user.CreditCards = cards;
            }
            if (addressIds != null)
            {
                dtoAddresses = await addressRepository.ListAsync(addressIds);
                user.Address = AddressMapping.ConvertDtoAddressToAddress(dtoAddresses.FirstOrDefault());
            }

            return user;
        }

        private async Task<IEnumerable<Card>> MapAddressesToCardsAsync(IEnumerable<Models.Repository.Card> cards)
        {
            var cardList = new List<Card>();
            foreach (var dtoCard in cards)
            {
                var address = await addressRepository.GetAsync(dtoCard.AddressId);
                var card = CardMapping.ConvertDtoCardToCard(dtoCard);
                card.BillingAddress = AddressMapping.ConvertDtoAddressToAddress(address);
                cardList.Add(card);
            }

            return cardList;
        }

        public async Task UpdateUserAccountAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await userRepository.UpdateAsync(UserMapping.CreateDtoUser(user));
        }

        public async Task UpdateUserPasswordAsync(Guid userId, string newPassword)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            await userRepository.UpdatePasswordAsync(userId, newPassword);
        }

        public async Task DeleteUserAccount(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            await userRepository.DeleteAsync(UserMapping.CreateDtoUser(user));
        }

        public async Task<bool> CheckIfUserEmailExists(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException();

            return await userRepository.CheckIfUserEmailExists(email);
        }

        public async Task AddCardAsync(Card card, Guid userId)
        {
            if (card == null || userId == Guid.Empty)
                throw new ArgumentNullException();

            var cardId = await cardRepository.AddAsync(CardMapping.CreateDtoCard(card));
            var addressId = await addressRepository.AddAsync(AddressMapping.CreateDtoAddress(card.BillingAddress));
            await userCardRepository.AddAsync(CardMapping.CreateUserCardDTO(userId, cardId));
            await cardAddressRepository.AddAsync(AddressMapping.CreateCardAddressDTO(userId, cardId));
        }

        public async Task UpdateCardAsync(Card card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            await cardRepository.UpdateAsync(CardMapping.CreateDtoCard(card));
        }
    }
}
