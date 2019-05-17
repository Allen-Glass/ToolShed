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

        public UserCartDataService(UserCartRepository userCartRepository,
            UserCartItemsRepository userCartItemsRepository)
        {
            this.userCartRepository = userCartRepository;
            this.userCartItemsRepository = userCartItemsRepository;
        }

        public async Task SaveUserCartAsync(UserCart userCart)
        {
            var userHasCart = await userCartRepository.DoesUserHaveItemsInCart(userCart.UserId);

            if (!userHasCart)
            {
                var dtoUserCart = UserCartMapping.CreateUserCartDto(userCart);
                await userCartRepository.AddUserCartAsync(dtoUserCart);
            }
        }
    }
}
