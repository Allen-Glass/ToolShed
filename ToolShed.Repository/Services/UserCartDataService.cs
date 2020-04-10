using System;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class UserCartDataService : IUserCartDataService
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
            this.userCartRepository = userCartRepository ?? throw new ArgumentNullException(nameof(userCartRepository));
            this.userCartItemsRepository = userCartItemsRepository ?? throw new ArgumentNullException(nameof(userCartItemsRepository));
            this.userCartItemRentalsRepository = userCartItemRentalsRepository ?? throw new ArgumentNullException(nameof(userCartItemRentalsRepository));
            this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            this.itemRentalDetailsRepository = itemRentalDetailsRepository ?? throw new ArgumentNullException(nameof(itemRentalDetailsRepository));
        }

        public async Task SaveUserCartAsync(UserCart userCart, CancellationToken cancellationToken = default)
        {
            var userHasCart = await userCartRepository.DoesUserHaveItemsInCart(userCart.UserId, cancellationToken);

            if (!userHasCart)
            {
                var dtoUserCart = userCart.CreateUserCartDto();
                await userCartRepository.AddAsync(dtoUserCart, cancellationToken);
            }
            if (userCart.ItemIds != null)
            {
                await userCartItemsRepository.AddAsync(userCart.UserCartId, userCart.ItemIds, cancellationToken);
            }
            if (userCart.ItemRentalIds != null)
            {
                await userCartItemRentalsRepository.AddAsync(userCart.UserCartId, userCart.ItemRentalIds, cancellationToken);
            }
        }

        public async Task<int> GetItemCountInCart(UserCart userCart, CancellationToken cancellationToken = default)
        {
            return await GetItemCountInCart(userCart.UserCartId, cancellationToken);
        }

        public async Task<int> GetItemCountInCart(Guid userCartId, CancellationToken cancellationToken = default)
        {
            var itemCount = userCartItemsRepository.GetCountAsync(userCartId, cancellationToken);
            var itemRentalCount = userCartItemRentalsRepository.GetItemCountInCartAsync(userCartId, cancellationToken);

            return await itemCount + await itemRentalCount;
        }

        public async Task<UserCart> GetUserCartAsync(UserCart userCart, CancellationToken cancellationToken = default)
        {
            userCart.ItemIds = await userCartItemsRepository.ListIdsAsync(userCart.UserCartId, cancellationToken);
            userCart.ItemRentalIds = await userCartItemRentalsRepository.ListIdsAsync(userCart.UserCartId, cancellationToken);

            var items = await itemRepository.ListAsync(userCart.ItemIds, cancellationToken);
            var itemRentalDetails = await itemRentalDetailsRepository.ListAsync(userCart.ItemRentalIds, cancellationToken);

            return userCart;
        }

        public async Task<UserCart> GetUserCartAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var userCart = await userCartRepository.GetAsync(userId, cancellationToken);
            return await GetUserCartAsync(userCart.ConvertUserCart());
        }

        public async Task<Guid> GetUserCartIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await userCartRepository.GetUserCartIdAsync(userId, cancellationToken);
        }

        public async Task UpdateUserCartAsync(UserCart userCart, CancellationToken cancellationToken = default)
        {
            if (userCart.UserCartId == Guid.Empty && userCart.UserId != Guid.Empty)
            {
                userCart.UserCartId = await userCartRepository.GetUserCartIdAsync(userCart.UserId, cancellationToken);
            }

            if (userCart.ItemIds != null)
            {
                await userCartItemsRepository.AddAsync(userCart.UserCartId, userCart.ItemIds, cancellationToken);
            }

            if (userCart.ItemRentalIds != null)
            {
                await userCartItemRentalsRepository.AddAsync(userCart.UserCartId, userCart.ItemRentalIds, cancellationToken);
            }
        }

        public async Task DeleteUserCartAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var userCartId = await userCartRepository.GetUserCartIdAsync(userId, cancellationToken);
            await userCartItemRentalsRepository.DeleteAsync(userCartId, cancellationToken);
            await userCartItemsRepository.DeleteUserCartItemsAsync(userCartId, cancellationToken);
            await userCartRepository.DeleteAsync(userId, cancellationToken);
        }
    }
}
