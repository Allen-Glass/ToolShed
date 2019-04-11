using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class OrderDetailsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public OrderDetailsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddOrderDetailsAsync(OrderDetails orderDetails)
        {
            if (orderDetails == null)
                throw new ArgumentNullException();

            await toolShedContext.OrderDetailsSet
                .AddAsync(orderDetails);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<OrderDetails> GetOrderDetailsAsync(Guid orderDetailsId)
        {
            if (orderDetailsId == Guid.Empty)
                throw new ArgumentNullException();

            var orderDetail = await toolShedContext.OrderDetailsSet
                .FirstOrDefaultAsync(c => c.OrderDetailsId.Equals(orderDetailsId));

            if (orderDetailsId == null)
                throw new NullReferenceException();

            return orderDetail;
        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetailsAsync(IEnumerable<Guid> orderDetailIds)
        {
            if (orderDetailIds == null)
                throw new ArgumentNullException();

            var orderDetails = new List<OrderDetails>();
            foreach (var orderDetailId in orderDetailIds)
            {
                orderDetails.Add(await GetOrderDetailsAsync(orderDetailId));
            }

            return orderDetails;
        }
    }
}
