using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;

namespace ToolShed.Services.Orders
{
    public class OrderItemService
    {
        public OrderItemService(IOrderDataService orderSQLService)
        {

        }

        public async Task PlaceOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException();


        }
    }
}
