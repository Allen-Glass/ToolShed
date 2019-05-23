using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Repository.Services;

namespace ToolShed.Services.UserCart
{
    public class UserCartService
    {
        private readonly UserCartDataService userCartDataService;

        public UserCartService(UserCartDataService userCartDataService)
        {
            this.userCartDataService = userCartDataService;
        }


    }
}
