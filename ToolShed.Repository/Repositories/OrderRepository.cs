using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class OrderRepository
    {
        private readonly ToolShedContext toolShedContext;

        public OrderRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException();

            await toolShedContext.OrderSet
                .AddAsync(order);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<Order> GetAsync(Guid orderId)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentNullException();

            var order = await toolShedContext.OrderSet
                .FirstOrDefaultAsync(c => c.OrderId.Equals(orderId));

            if (order == null)
                throw new NullReferenceException();

            return order;
        }

        public async Task<IEnumerable<Order>> ListAsync(IEnumerable<Guid> orderIds)
        {
            if (orderIds == null)
                throw new ArgumentNullException();

            var orders = new List<Order>();
            foreach (var orderId in orderIds)
            {
                orders.Add(await GetAsync(orderId));
            }

            return orders;
        }

        public async Task UpdateAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException();
        }

        public async Task UpdateAsync(Guid orderId)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentNullException();

            var order = await GetAsync(orderId);
        }
    }
}
