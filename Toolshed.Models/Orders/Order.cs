using System;

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
        //public User User { get; set; }

        /// <summary>
        /// price of order
        /// </summary>
        public double Price { get; set; }
    }
}
