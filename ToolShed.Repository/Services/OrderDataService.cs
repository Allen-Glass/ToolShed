using System;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class OrderDataService : IOrderDataService
    {
        private readonly OrderRepository orderRepository;
        private readonly OrderDetailsRepository orderDetailsRepository;
        private readonly OrderRecordRepository orderRecordRepository;

        public OrderDataService(OrderRepository orderRepository,
            OrderDetailsRepository orderDetailsRepository,
            OrderRecordRepository orderRecordRepository)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.orderDetailsRepository = orderDetailsRepository ?? throw new ArgumentNullException(nameof(orderDetailsRepository));
            this.orderRecordRepository = orderRecordRepository ?? throw new ArgumentNullException(nameof(orderRecordRepository));
        }

        public async Task AddOrderAsync(UserOrder userOrder, CancellationToken cancellationToken = default)
        {
            if (userOrder == null)
                throw new ArgumentNullException(nameof(userOrder));

            await orderRepository.AddAsync(OrderMapping.CreateDtoOrder(userOrder.Order));
            await orderDetailsRepository.AddAsync(OrderMapping.CreateDtoOrderDetail(userOrder.OrderDetail));
            await orderRecordRepository.AddAsync(OrderMapping.CreateDtoRecord(userOrder), cancellationToken);
        }

        public async Task UpdateOrderStateAsync(UserOrder userOrder, CancellationToken cancellationToken = default)
        {
            if (userOrder == null)
                throw new ArgumentNullException(nameof(userOrder));

            await orderRepository.UpdateAsync(OrderMapping.CreateDtoOrder(userOrder.Order), cancellationToken);
            await orderRecordRepository.AddAsync(OrderMapping.CreateDtoRecord(userOrder), cancellationToken);
        }

        public async Task UpdateOrderDetailsAsync(UserOrder userOrder, CancellationToken cancellationToken = default)
        {
            if (userOrder == null)
                throw new ArgumentNullException(nameof(userOrder));

            await orderDetailsRepository.UpdateAsync(OrderMapping.CreateDtoOrderDetail(userOrder.OrderDetail), cancellationToken);
            await orderRecordRepository.AddAsync(OrderMapping.CreateDtoRecord(userOrder), cancellationToken);
        }
    }
}
