using System;
using ToolShed.Models.Enums;

namespace ToolShed.Models.API
{
    public class Rental
    {
        /// <summary>
        /// pk of rental
        /// </summary>
        public Guid RentalId { get; set; }

        /// <summary>
        /// the user renting
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Rental Item
        /// </summary>
        public ItemRentalDetails ItemRentalDetails { get; set; }

        /// <summary>
        /// the dispenser associated with the id
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// the time the rental started
        /// </summary>
        public DateTime RentalStartTime { get; set; }
        
        /// <summary>
        /// The time the rental is due
        /// </summary>
        public DateTime RentalDueTime { get; set; }
        
        /// <summary>
        /// Time the rental is returned
        /// </summary>
        public DateTime RentalReturnTime { get; set; }

        /// <summary>
        /// the length of the rental
        /// </summary>
        public int RentalDuration { get; set; }

        /// <summary>
        /// The payment associated with the rental
        /// </summary>
        public Payment Payment { get; set; }

        /// <summary>
        /// the type of return (overdue, on time, full price, etc...)
        /// </summary>
        public ReturnType ReturnType { get; set; }

        /// <summary>
        /// User has returned the rental
        /// </summary>
        public bool HasBeenReturned { get; set; }

        /// <summary>
        /// for the users that don't return things
        /// </summary>
        public bool IsUserOwnedNow { get; set; }

        /// <summary>
        /// unique locker code for rental
        /// </summary>
        public string LockerCode { get; set; }
    }
}
