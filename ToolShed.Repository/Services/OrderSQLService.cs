using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class OrderSQLService : IOrderSQLService
    {
        private readonly OrderRepository orderRepository;
        private readonly OrderDetailsRepository orderDetailsRepository;

        public OrderSQLService(OrderRepository orderRepository,
            OrderDetailsRepository orderDetailsRepository)
        {
            this.orderRepository = orderRepository;
            this.orderDetailsRepository = orderDetailsRepository;
        }

        public async Task AddOrderAsync(Order order)
        {

        }
    }
}
