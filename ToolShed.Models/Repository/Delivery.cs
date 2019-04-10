using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.Repository
{
    public class Delivery
    {
        public Guid DeliveryId { get; set; }

        public Guid DeliveryAddressId { get; set; }

        public string DisplayName { get; set; }

        public double Sale { get; set; }

        public Guid DispenserId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public DateTime SentDate { get; set; }

        public string DeliveryProvider { get; set; }
    }
}
