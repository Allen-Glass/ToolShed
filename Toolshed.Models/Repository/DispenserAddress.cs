using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    /// <summary>
    /// table that relates dispensers to address
    /// </summary>
    public class DispenserAddress
    {
        /// <summary>
        /// pk of dispenser address
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DispenserAddressId { get; set; }

        /// <summary>
        /// pk of dispenser
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// pk of address
        /// </summary>
        public Guid AddressId { get; set; }
    }
}
