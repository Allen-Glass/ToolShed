using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Services.Interfaces.Orders;

namespace ToolShed.Services.Orders
{
    public class UserCartService : IUserCartService
    {
        public UserCartService()
        {

        }

        public async Task SaveCartAsync(UserCart userCart)
        {
            if (userCart.UserCartId == Guid.Empty)
                throw new ArgumentNullException();


        }
    }
}
