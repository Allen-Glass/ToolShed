﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolshed.Models.Enums;

namespace ToolShed.Models.Repository
{
    public class Address
    {
        /// <summary>
        /// pk of address stored
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AddressId { get; set; }

        /// <summary>
        /// street name information
        /// </summary>
        public string StreetName { get; set; }

        /// <summary>
        /// second street name
        /// </summary>
        public string StreetName2 { get; set; }

        /// <summary>
        /// appartment number
        /// </summary>
        public string AptNumber { get; set; }

        /// <summary>
        /// country of user
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// the state of user
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// User's city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// zip code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// the object associated with this address
        /// </summary>
        public AddressType AddressType { get; set; }
    }
}
