using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;

namespace ToolShed.Services.Orders
{
    public class OrderService
    {
        private readonly IOrderDataService orderDataService;

        public OrderService(IOrderDataService orderDataService)
        {
            this.orderDataService = orderDataService ?? throw new ArgumentNullException(nameof(orderDataService));
        }

        public async Task PlaceOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

        }
    }
}
