using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class OrderDataService : IOrderDataService
    {
        private readonly OrderRepository orderRepository;
        private readonly OrderDetailsRepository orderDetailsRepository;

        public OrderDataService(OrderRepository orderRepository,
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
