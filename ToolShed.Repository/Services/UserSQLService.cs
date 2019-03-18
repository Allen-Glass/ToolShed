using System;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using ToolShed.Models.API;
using ToolShed.Repository.Mapping;
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

            var userId = await userRepository.AddUserAsync(UserMapping.ConvertUserToDtoUser(user));
            if (user.Address != null)
            {
                var addressId = await addressRepository.AddAddressAsync(AddressMapping.CreateDtoAddress(user.Address));
                await userAddressesRepository.AddUserAddressAsync(AddressMapping.CreateUserAddressDTO(userId, addressId));
            }
        }

        public async Task StoreCreditCardAsync(Card card, Guid userId)
        {
            if (card == null || userId == Guid.Empty)
                throw new ArgumentNullException();

            var cardId = await cardRepository.AddCardAsync(CardMapping.CreateDtoCard(card));
            var addressId = await addressRepository.AddAddressAsync(AddressMapping.CreateDtoAddress(card.BillingAddress));
            await userCardRepository.AddUserCardAsync(CardMapping.CreateUserCardDTO(userId, cardId));
            await cardAddressRepository.AddCardAddressAsync(AddressMapping.CreateCardAddressDTO(userId, cardId));
        }

        public async Task DeleteUserAccount(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            await userRepository.DeleteUserAsync(UserMapping.ConvertUserToDtoUser(user));
        }
    }
}
