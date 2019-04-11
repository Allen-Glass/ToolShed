using System;

namespace ToolShed.Models.Repository
{
    public class OrderDetails
    {
        public Guid OrderDetailsId { get; set; }

        public Guid OrderId { get; set; }
        
        public Guid ItemId { get; set; }

        public double Pricing { get; set; }
    }
}
