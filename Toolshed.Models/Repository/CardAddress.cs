using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToolShed.Models.Repository
{
    /// <summary>
    /// relates address to cards
    /// </summary>
    public class CardAddress
    {
        /// <summary>
        /// card address id pk
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
