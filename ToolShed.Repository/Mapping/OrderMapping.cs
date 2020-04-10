using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class OrderMapping
    {
        public static Models.Repository.Order CreateDtoOrder(this Order order)
        {
            return new Models.Repository.Order
            {
                OrderName = order.OrderName,
                OrderStatus = order.OrderStatus
            };
        }

        public static IEnumerable<Models.Repository.Order> CreateDtoOrders(this IEnumerable<Order> orders)
        {
            var orderList = new List<Models.Repository.Order>();
            foreach(var order in orders)
            {
                orderList.Add(CreateDtoOrder(order));
            }

            return orderList;
        }

        public static Models.Repository.OrderDetail CreateDtoOrderDetail(this OrderDetail order)
        {
            return new Models.Repository.OrderDetail
            {
            };
        }

        public static IEnumerable<Models.Repository.OrderDetail> CreateDtoOrderDetails(this IEnumerable<OrderDetail> orders)
        {
            var orderList = new List<Models.Repository.OrderDetail>();
            foreach (var order in orders)
            {
                orderList.Add(CreateDtoOrderDetail(order));
            }

            return orderList;
        }

        public static Models.Repository.OrderRecord CreateDtoRecord(this UserOrder userOrder)
        {
            return new Models.Repository.OrderRecord
            {

            };
        }

        public static Order ConvertDtoOrder(this Models.Repository.Order order)
        {
            return new Order
            {
                OrderId = order.OrderId,
                OrderName = order.OrderName,
                OrderStatus = order.OrderStatus
            };
        }

        public static IEnumerable<Order> ConvertDtoOrders(this IEnumerable<Models.Repository.Order> orders)
        {
            var orderList = new List<Order>();
            foreach (var order in orders)
            {
                orderList.Add(ConvertDtoOrder(order));
            }

            return orderList;
        }

        public static OrderDetail ConvertDtoOrderDetail(this Models.Repository.OrderDetail order)
        {
            return new OrderDetail
            {
            };
        }

        public static IEnumerable<OrderDetail> ConvertDtoOrderDetails(this IEnumerable<Models.Repository.OrderDetail> orders)
        {
            var orderList = new List<OrderDetail>();
            foreach (var order in orders)
            {
                orderList.Add(ConvertDtoOrderDetail(order));
            }

            return orderList;
        }
    }
}
