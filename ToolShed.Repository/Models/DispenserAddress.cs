﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Toolshed.Repository.Models
{
    /// <summary>
    /// table that relates dispensers to address
    /// </summary>
    public class DispenserAddress
    {
        /// <summary>
        /// pk of dispenser address
        /// </summary>
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