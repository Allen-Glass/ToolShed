using System;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Repository
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string OrderName { get; set; }
    }
}
