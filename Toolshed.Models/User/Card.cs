using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Toolshed.Models.User
{
    /// <summary>
    /// Credit card object
    /// </summary>
    public class Card
    {
        /// <summary>
        /// pk of credit card
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CardId { get;set; }

        /// <summary>
        /// Credit Card number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// CCV of a credit card
        /// </summary>
        public string CCV { get; set; }

        /// <summary>
        /// Card holder name
        /// </summary>
        public string CardHolderName { get; set; }

        /// <summary>
        /// Billing Address of credit card
        /// </summary>
        public Address BillingAddress { get; set; }
    }
}
