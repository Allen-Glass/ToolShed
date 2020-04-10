using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.API
{
    /// <summary>
    /// tenant object
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
        public Address Address { get; set; }
    }
}
