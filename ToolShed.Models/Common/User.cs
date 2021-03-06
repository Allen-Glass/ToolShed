﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.API
{
    /// <summary>
    /// User uploaded information
    /// </summary>
    public class User
    {
        /// <summary>
        /// pk of user
        /// </summary>
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
        /// unique username of user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// user's password salt
        /// </summary>
        public string PasswordSalt { get; set; }

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
