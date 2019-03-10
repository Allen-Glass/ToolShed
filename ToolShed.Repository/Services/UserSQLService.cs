using System;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using ToolShed.Models.API;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class UserSQLService
    {
        private readonly UserRepository userRepository;
        private readonly AddressRepository addressRepository;
        private readonly CardRepository cardRepository;
        private readonly UserCardRepository userCardRepository;
        private readonly UserAddressesRepository userAddressesRepository;
        private readonly CardAddressRepository cardAddressRepository;

        public UserSQLService(UserRepository userRepository
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

        public async Task StoreUserInformationAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            var userId = await userRepository.AddUserAsync(ConvertUserToDtoUser(user));
            if (user.Address != null)
            {
                var addressId = await addressRepository.AddAddressAsync(ConvertAddressToDtoAddress(user.Address));
                await userAddressesRepository.AddUserAddressAsync(CreateUserAddressDTO(userId, addressId));
            }
        }

        public async Task StoreCreditCardAsync(Card card, Guid userId)
        {
            if (card == null || userId == Guid.Empty)
                throw new ArgumentNullException();

            var cardId = await cardRepository.AddCardAsync(ConvertCardToDtoCard(card));
            var addressId = await addressRepository.AddAddressAsync(ConvertAddressToDtoAddress(card.BillingAddress));
            await userCardRepository.AddUserCardAsync(CreateUserCardDTO(userId, cardId));
            await cardAddressRepository.AddCardAddressAsync(CreateCardAddressDTO(userId, cardId));
        }

        public async Task DeleteUserAccount(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            await userRepository.DeleteUserAsync(ConvertUserToDtoUser(user));
        }

        private Models.Repository.User ConvertUserToDtoUser(User user)
        {
            return new Models.Repository.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        private Models.Repository.UserAddresses CreateUserAddressDTO(Guid userId, Guid addressId)
        {
            return new Models.Repository.UserAddresses
            {
                UserId = userId,
                AddressId = addressId
            };
        }

        private Models.Repository.UserCard CreateUserCardDTO(Guid userId, Guid cardId)
        {
            return new Models.Repository.UserCard
            {
                UserId = userId,
                CardId = cardId
            };
        }

        private Models.Repository.CardAddress CreateCardAddressDTO(Guid addressId, Guid cardId)
        {
            return new Models.Repository.CardAddress
            {
                AddressId = addressId,
                CardId = cardId
            };
        }

        private Models.Repository.User ConvertUserToDtoUser(User user, Guid addressId)
        {
            return new Models.Repository.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AddressId = addressId
            };
        }

        private Models.Repository.Address ConvertAddressToDtoAddress(Address address)
        {
            return new Models.Repository.Address
            {
                AddressType = AddressType.user,
                AptNumber = address.AptNumber,
                City = address.City,
                Country = address.Country,
                State = address.State,
                StreetName = address.StreetName,
                StreetName2 = address.StreetName2,
                ZipCode = address.ZipCode
            };
        }

        private Models.Repository.Card ConvertCardToDtoCard(Card card)
        {
            return new Models.Repository.Card
            {
                CardHolderName = card.CardHolderName,
                CardNumber = card.CardNumber,
                UserId = card.UserId
            };
        }
    }
}
