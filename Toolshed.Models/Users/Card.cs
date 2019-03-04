using System;

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
        public Guid CardId { get;set; }

        /// <summary>
        /// userId associated with card
        /// </summary>
        public Guid UserId { get; set; }

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
