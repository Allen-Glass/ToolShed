using System;
using Toolshed.Models.Accounts;

namespace Toolshed.Models.Orders
{
    /// <summary>
    /// User submitted order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// unique order identifier
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// User account
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// price of order
        /// </summary>
        public double Price { get; set; }
    }
}
