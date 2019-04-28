using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.API
{
    public class OrderDetail
    {
        public Guid OrderDetailsId { get; set; }

        public Guid OrderId { get; set; }

        public Guid ItemId { get; set; }

        public double Pricing { get; set; }
    }
}
