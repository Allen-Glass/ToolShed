using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Toolshed.Models.Maintenance
{
    /// <summary>
    /// The individual providing maintenance
    /// </summary>
    public class MaintenanceProvider
    {
        /// <summary>
        /// pk of maintenance provider id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MaintenanceProviderId { get; set; }

        /// <summary>
        /// the first name of the individual providing maintenance
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// the last name of the individual providing maintenance
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// the email of the individual providing maintenance
        /// </summary>
        public string Email { get; set; }
    }
}
