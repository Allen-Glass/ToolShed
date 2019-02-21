using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Toolshed.Models.User
{
    /// <summary>
    /// User uploaded information
    /// </summary>
    public class UserInformation
    {
        /// <summary>
        /// User's first name
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// user address
        /// </summary>
        public Address Address { get; set; }
    }
}
