﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
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
        /// user name of account
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// user address pk
        /// </summary>
        public Guid AddressId { get; set; }
    }
}
