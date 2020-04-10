using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.API
{
    public class UserOrder
    {
        public Order Order { get; set; }

        public OrderDetail OrderDetail { get; set; }
    }
}
