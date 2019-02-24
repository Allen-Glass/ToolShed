using System;
using System.Collections.Generic;
using System.Text;

namespace Toolshed.Models.SQL
{
    /// <summary>
    /// relates address to cards
    /// </summary>
    public class CardAddress
    {
        /// <summary>
        /// card address id pk
        /// </summary>
        public Guid CardAddressesId { get; set; }

        /// <summary>
        /// pk of card id
        /// </summary>
        public Guid CardId { get; set; }

        /// <summary>
        /// pk of address
        /// </summary>
        public Guid AddressId { get; set; }
    }
}
