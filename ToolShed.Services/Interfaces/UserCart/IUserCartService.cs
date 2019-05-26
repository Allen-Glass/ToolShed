using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToolShed.Services.Interfaces.UserCart
{
    public interface IUserCartService
    {
        Task SaveUserCartAsync(Models.API.UserCart userCart);

        Task<Models.API.UserCart> GetUserCartAsync(Guid userId);

        Task<int> GetUserItemCount(Guid userId);

        Task UpdateItemsInUserCartAsync(Models.API.UserCart userCart);

        Task DeleteUserCartAsync(Guid userId);
    }
}
