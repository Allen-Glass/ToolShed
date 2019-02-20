using System;
using System.Collections.Generic;
using System.Text;

namespace Toolshed.Models.Web
{
    /// <summary>
    /// User uploaded information
    /// </summary>
    public class UserInformation
    {
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
    }
}
