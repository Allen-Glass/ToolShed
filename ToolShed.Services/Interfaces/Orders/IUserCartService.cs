using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Services.Interfaces.Orders
{
    public interface IUserCartService
    {
        /// <summary>
        /// save user cart information
        /// </summary>
        /// <param name="userCart"></param>
        /// <returns></returns>
        Task SaveCartAsync(UserCart userCart);
    }
}
