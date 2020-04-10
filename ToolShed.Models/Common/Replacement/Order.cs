using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.API;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Common.Replacement
{
    public class Order
    {
        public Address DeliveryAddress { get; set; }

        public Address BillingAddress { get; set; }

        public string DisplayName { get; set; }

        public Tenant Tenant { get; set; }

        public double TotalCost { get; set; }

        public double SalesTax { get; set; }

        public ReplacementDetails ReplacementDetails { get; set; }

        public IEnumerable<DateTime> PreviousDeliveryDates { get; set; }

        public DateTime NextDeliveryDate { get; set; }
    }
}
