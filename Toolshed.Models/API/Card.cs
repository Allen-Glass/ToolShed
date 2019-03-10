using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShed.Models.API
{
    public class Card
    {
        /// <summary>
        /// pk of card
        /// </summary>
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
        public Address BillingAddress { get; set; }
    }
}
