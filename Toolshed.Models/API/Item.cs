using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.API
{
    /// <summary>
    /// generic term to indicate item to be sold or vended
    /// </summary>
    public class Item
    {
        /// <summary>
        /// pk of item
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// the owning tenant
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// the dispenser at which the item is located
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// name of the item
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// item location
        /// </summary>
        public string ItemLocker { get; set; }

        /// <summary>
        /// is tool available
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// can this tool be rented
        /// </summary>
        public bool IsRentable { get; set; }

        /// <summary>
        /// the tool is damaged
        /// </summary>
        public bool IsDamaged { get; set; }

        /// <summary>
        /// the total sale price of the tool
        /// </summary>
        public double SalePrice { get; set; }

        /// <summary>
        /// the total price to buy
        /// </summary>
        public double BuyPrice { get; set; }

        /// <summary>
        /// The cost per hour
        /// </summary>
        public double PricePerHour { get; set; }

        /// <summary>
        /// the default duration of the base rental
        /// </summary>
        public double BaseRentalPeriodDuration { get; set; }

        /// <summary>
        /// the cost per hour after missed due time
        /// </summary>
        public double PricePerHourOver { get; set; }
    }
}
