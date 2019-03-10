using System;

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
        public Guid UserAddressesId { get; set; }

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
