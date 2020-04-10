using System;
using System.Collections.Generic;
using ToolShed.Models.Enums;

namespace ToolShed.Models.API
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public string OrderName { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public bool IsValid { get; set; }

        public IEnumerable<ItemRentalDetails> ItemRentalDetails { get; set; }
    }
}
