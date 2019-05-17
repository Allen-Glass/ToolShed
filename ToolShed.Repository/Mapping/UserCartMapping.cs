using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class UserCartMapping
    {
        public static Models.Repository.UserCart CreateUserCartDto(UserCart userCart)
        {
            return new Models.Repository.UserCart
            {
                UserId = userCart.UserId
            };
        }

        public static UserCart ConvertUserCart(Models.Repository.UserCart userCart)
        {
            return new UserCart
            {
                UserId = userCart.UserId,
                UserCartId = userCart.UserCartId
            };
        }
    }
}
