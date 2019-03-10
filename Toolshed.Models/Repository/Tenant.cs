using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToolShed.Models.Repository
{
    /// <summary>
    /// Tenant DTO
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// unique key of tenant
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TenantId { get; set; }

        /// <summary>
        /// Name of tenant company
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// Location of tenant
        /// </summary>
        public Guid TenantAddressId { get; set; }
    }
}
