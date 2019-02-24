using System;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using Toolshed.Models.User;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class UserSQLService : IUserSQLService
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
            var userId = await userRepository.AddUserAsync(ConvertUserToDtoUser(user));
            if (user.Address != null)
            {
                var addressId = await addressRepository.AddAddressAsync(ConvertAddressToDtoAddress(user.Address));
                await userAddressesRepository.AddUserAddressAsync(CreateUserAddressDTO(userId, addressId));
            }
        }

        public async Task StoreCreditCardAsync(Card card, Guid userId)
        {
            var cardId = await cardRepository.AddCardAsync(ConvertCardToDtoCard(card));
            var addressId = await addressRepository.AddAddressAsync(ConvertAddressToDtoAddress(card.BillingAddress));
            await userCardRepository.AddUserCardAsync(CreateUserCardDTO(userId, cardId));
            await cardAddressRepository.AddCardAddressAsync(CreateCardAddressDTO(userId, cardId));
        }

        public async Task DeleteUserAccount(User user)
        {
            await userRepository.DeleteUserAsync(ConvertUserToDtoUser(user));
        }

        private Toolshed.Models.SQL.User ConvertUserToDtoUser(User user)
        {
            return new Toolshed.Models.SQL.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        private Toolshed.Models.SQL.UserAddresses CreateUserAddressDTO(Guid userId, Guid addressId)
        {
            return new Toolshed.Models.SQL.UserAddresses
            {
                UserId = userId,
                AddressId = addressId
            };
        }

        private Toolshed.Models.SQL.UserCard CreateUserCardDTO(Guid userId, Guid cardId)
        {
            return new Toolshed.Models.SQL.UserCard
            {
                UserId = userId,
                CardId = cardId
            };
        }

        private Toolshed.Models.SQL.CardAddress CreateCardAddressDTO(Guid addressId, Guid cardId)
        {
            return new Toolshed.Models.SQL.CardAddress
            {
                AddressId = addressId,
                CardId = cardId
            };
        }

        private Toolshed.Models.SQL.User ConvertUserToDtoUser(User user, Guid addressId)
        {
            return new Toolshed.Models.SQL.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AddressId = addressId
            };
        }

        private Toolshed.Models.SQL.Address ConvertAddressToDtoAddress(Address address)
        {
            return new Toolshed.Models.SQL.Address
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

        private Toolshed.Models.SQL.Card ConvertCardToDtoCard(Card card)
        {
            return new Toolshed.Models.SQL.Card
            {
                CardHolderName = card.CardHolderName,
                CardNumber = card.CardNumber,
                UserId = card.UserId
            };
        }
    }
}
