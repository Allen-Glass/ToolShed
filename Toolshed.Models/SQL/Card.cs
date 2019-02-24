using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toolshed.Models.SQL
{
    /// <summary>
    /// credit card sql object
    /// </summary>
    public class Card
    {
        /// <summary>
        /// pk of credit card
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CardId { get; set; }

        /// <summary>
        /// userId associated with card
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Credit Card number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Card holder name
        /// </summary>
        public string CardHolderName { get; set; }

        /// <summary>
        /// Billing Address of credit card
        /// </summary>
        public Guid BillingAddressId { get; set; }
    }
}
