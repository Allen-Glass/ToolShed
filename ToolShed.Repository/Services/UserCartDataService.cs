using System;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class UserCartDataService
    {
        private readonly UserCartRepository userCartRepository;
        private readonly UserCartItemsRepository userCartItemsRepository;
        private readonly UserCartItemRentalsRepository userCartItemRentalsRepository;
        private readonly ItemRepository itemRepository;
        private readonly ItemRentalDetailsRepository itemRentalDetailsRepository;

        public UserCartDataService(UserCartRepository userCartRepository,
            UserCartItemsRepository userCartItemsRepository,
            UserCartItemRentalsRepository userCartItemRentalsRepository,
            ItemRepository itemRepository,
            ItemRentalDetailsRepository itemRentalDetailsRepository)
        {
            this.userCartRepository = userCartRepository;
            this.userCartItemsRepository = userCartItemsRepository;
            this.userCartItemRentalsRepository = userCartItemRentalsRepository;
            this.itemRepository = itemRepository;
            this.itemRentalDetailsRepository = itemRentalDetailsRepository;
        }

        public async Task SaveUserCartAsync(UserCart userCart)
        {
            var userHasCart = await userCartRepository.DoesUserHaveItemsInCart(userCart.UserId);

            if (!userHasCart)
            {
                var dtoUserCart = UserCartMapping.CreateUserCartDto(userCart);
                await userCartRepository.AddUserCartAsync(dtoUserCart);
            }
            if (userCart.ItemIds != null)
            {
                await userCartItemsRepository.AddUserCartItemsAsync(userCart.UserCartId, userCart.ItemIds);
            }
            if (userCart.ItemRentalIds != null)
            {
                await userCartItemRentalsRepository.AddUserRentalItemAsync(userCart.UserCartId, userCart.ItemRentalIds);
            }
        }

        public int GetItemCountInCart(UserCart userCart)
        {
            return GetItemCountInCart(userCart.UserCartId);
        }

        public int GetItemCountInCart(Guid userCartId)
        {
            var itemCount = userCartItemsRepository.GetItemCountInCartAsync(userCartId);
            var itemRentalCount = userCartItemRentalsRepository.GetItemCountInCartAsync(userCartId);

            return itemCount + itemRentalCount;
        }

        public async Task<UserCart> GetUserCartAsync(UserCart userCart)
        {
            if (userCart.UserCartId == Guid.Empty)
                throw new ArgumentNullException();

            userCart.ItemIds = await userCartItemsRepository.GetUserCartItemIds(userCart.UserCartId);
            userCart.ItemRentalIds = await userCartItemRentalsRepository.GetItemRentalIdsAsync(userCart.UserCartId);

            var items = await itemRepository.GetItemsByItemIdsAsync(userCart.ItemIds);
            var itemRentalDetails = await itemRentalDetailsRepository.GetItemRentalDetailsAsync(userCart.ItemRentalIds);

            return userCart;
        }
    }
}
