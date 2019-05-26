using System;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface IUserCartDataService
    {
        Task SaveUserCartAsync(UserCart userCart);

        int GetItemCountInCart(UserCart userCart);

        int GetItemCountInCart(Guid userCartId);

        Task<UserCart> GetUserCartAsync(UserCart userCart);

        Task<UserCart> GetUserCartAsync(Guid userId);

        Task<Guid> GetUserCartIdAsync(Guid userId);

        Task UpdateUserCartAsync(UserCart userCart);

        Task DeleteUserCartAsync(Guid userId);
    }
}
