using System;
using System.Threading.Tasks;
using ToolShed.Repository.Interfaces;
using ToolShed.Services.Interfaces.UserCart;

namespace ToolShed.Services.UserCart
{
    public class UserCartService : IUserCartService
    {
        private readonly IUserCartDataService userCartDataService;

        public UserCartService(IUserCartDataService userCartDataService)
        {
            this.userCartDataService = userCartDataService;
        }

        public async Task SaveUserCartAsync(Models.API.UserCart userCart)
        {
            if (userCart == null)
                throw new ArgumentNullException();

            if (userCart.ItemIds == null)
                throw new ArgumentNullException();

            await userCartDataService.SaveUserCartAsync(userCart);
        }

        public async Task<Models.API.UserCart> GetUserCartAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            return await userCartDataService.GetUserCartAsync(userId);
        }

        public async Task<int> GetUserItemCount(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            var cartId = await userCartDataService.GetUserCartIdAsync(userId);
            return userCartDataService.GetItemCountInCart(cartId);
        }

        public async Task UpdateItemsInUserCartAsync(Models.API.UserCart userCart)
        {
            if (userCart == null)
                throw new ArgumentNullException();

            if (userCart.ItemIds == null && userCart.ItemRentalIds == null)
            {
                throw new ArgumentNullException();
            }

            await userCartDataService.UpdateUserCartAsync(userCart);
        }

        public async Task DeleteUserCartAsync(Guid userId)
        {
            await userCartDataService.DeleteUserCartAsync(userId);
        }
    }
}
