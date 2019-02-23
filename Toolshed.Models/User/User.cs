using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toolshed.Models.User
{
    /// <summary>
    /// User uploaded information
    /// </summary>
    public class User
    {
        /// <summary>
        /// pk of user
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
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

        /// <summary>
        /// user credit cards
        /// </summary>
        public IEnumerable<Card> CreditCards { get; set; }
    }
}
