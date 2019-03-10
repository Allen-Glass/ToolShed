using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    /// <summary>
    /// Maintenance request object 
    /// </summary>
    public class MaintenanceRequest
    {
        /// <summary>
        /// pk of maintenance request
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MaintenanceRequestId { get; set; }

        /// <summary>
        /// pk of dispenser id
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// maintenance information
        /// </summary>
        public string MaintenanceInformation { get; set; }

        /// <summary>
        /// the name of the maintenance provider
        /// </summary>
        public string MaintenanceProviderName { get; set; }

        /// <summary>
        /// the pk of maintenance provider id
        /// </summary>
        public Guid MaintenanceProviderId { get; set; }

        /// <summary>
        /// Process is complete
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Time of creation
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Time of completion
        /// </summary>
        public DateTime CompletedOn { get; set; }
    }
}
