using System;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface IUserCartDataService
    {
        Task SaveUserCartAsync(UserCart userCart, CancellationToken cancellationToken = default);

        Task<int> GetItemCountInCart(UserCart userCart, CancellationToken cancellationToken = default);

        Task<int> GetItemCountInCart(Guid userCartId, CancellationToken cancellationToken = default);

        Task<UserCart> GetUserCartAsync(UserCart userCart, CancellationToken cancellationToken = default);

        Task<UserCart> GetUserCartAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Guid> GetUserCartIdAsync(Guid userId, CancellationToken cancellationToken = default);

        Task UpdateUserCartAsync(UserCart userCart, CancellationToken cancellationToken = default);

        Task DeleteUserCartAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
