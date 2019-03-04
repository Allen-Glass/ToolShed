using System;
using Toolshed.Models.Orders;

namespace Toolshed.Models.Scheduler
{
    /// <summary>
    /// Object for reserving tools
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// The user account renting the tool
        /// </summary>
        //public User User { get; set; }

        /// <summary>
        /// The time when the reservation starts
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The time of when the reservation ends
        /// </summary>
        public DateTime ReturnTime { get; set; }

        /// <summary>
        /// User submitted order
        /// </summary>
        public Order Order { get; set; }
    }
}
