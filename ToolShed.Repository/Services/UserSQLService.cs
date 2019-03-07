using System;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
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

        private Toolshed.Repository.Models.User ConvertUserToDtoUser(User user)
        {
            return new Toolshed.Repository.Models.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        private Toolshed.Repository.Models.UserAddresses CreateUserAddressDTO(Guid userId, Guid addressId)
        {
            return new Toolshed.Repository.Models.UserAddresses
            {
                UserId = userId,
                AddressId = addressId
            };
        }

        private Toolshed.Repository.Models.UserCard CreateUserCardDTO(Guid userId, Guid cardId)
        {
            return new Toolshed.Repository.Models.UserCard
            {
                UserId = userId,
                CardId = cardId
            };
        }

        private Toolshed.Repository.Models.CardAddress CreateCardAddressDTO(Guid addressId, Guid cardId)
        {
            return new Toolshed.Repository.Models.CardAddress
            {
                AddressId = addressId,
                CardId = cardId
            };
        }

        private Toolshed.Repository.Models.User ConvertUserToDtoUser(User user, Guid addressId)
        {
            return new Toolshed.Repository.Models.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AddressId = addressId
            };
        }

        private Toolshed.Repository.Models.Address ConvertAddressToDtoAddress(Address address)
        {
            return new Toolshed.Repository.Models.Address
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

        private Toolshed.Repository.Models.Card ConvertCardToDtoCard(Card card)
        {
            return new Toolshed.Repository.Models.Card
            {
                CardHolderName = card.CardHolderName,
                CardNumber = card.CardNumber,
                UserId = card.UserId
            };
        }
    }
}
