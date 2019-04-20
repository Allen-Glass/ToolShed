using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToolShed.Models.Enums;

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
        /// Rental Item
        /// </summary>
        public Guid ItemRentalDetailsId { get; set; }

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
        public Guid PaymentId { get; set; }

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
