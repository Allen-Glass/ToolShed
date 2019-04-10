using System;

namespace ToolShed.Models.Repository
{
    public class OrderDetail
    {
        public Guid OrderDetailId { get; set; }

        public Guid OrderId { get; set; }
        
        public Guid ItemId { get; set; }

        public double Pricing { get; set; }
    }
}
