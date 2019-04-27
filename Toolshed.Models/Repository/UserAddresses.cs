using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    /// <summary>
    /// Table that aligns address with user
    /// </summary>
    public class UserAddresses
    {
        /// <summary>
        /// pk of user address id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserAddressId { get; set; }

        /// <summary>
        /// pk of user
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// pk of address
        /// </summary>
        public Guid AddressId { get; set; }
    }
}
