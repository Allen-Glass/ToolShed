using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    public class Rental
    {
        /// <summary>
        /// pk of rental
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RentalId { get; set; }

        /// <summary>
        /// the user renting
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// the time the rental started
        /// </summary>
        public DateTime RentalStart { get; set; }

        /// <summary>
        /// The time the rental is due
        /// </summary>
        public DateTime RentalDue { get; set; }

        /// <summary>
        /// Time the rental is returned
        /// </summary>
        public DateTime RentalReturned { get; set; }

        /// <summary>
        /// The price per hour for rental
        /// </summary>
        public double PricePerHour { get; set; }

        /// <summary>
        /// The price per hour after missing return time
        /// </summary>
        public double PricePerHourOver { get; set; }

        /// <summary>
        /// The final cost of the return
        /// </summary>
        public double FinalCost { get; set; }

        /// <summary>
        /// User has returned the rental
        /// </summary>
        public bool HasBeenReturned { get; set; }

        /// <summary>
        /// for the users that don't return things
        /// </summary>
        public bool IsUserOwnedNow { get; set; }

        /// <summary>
        /// unique locker code for dispenser
        /// </summary>
        public string LockerCode { get; set; }
    }
}
