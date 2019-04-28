using System;
using ToolShed.Models.Enums;

namespace ToolShed.Models.API
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public string OrderName { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
