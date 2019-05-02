using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    /// <summary>
    /// Tool dispenser object
    /// </summary>
    public class Dispenser
    {
        /// <summary>
        /// pk of dispenser id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DispenserId { get; set; }

        /// <summary>
        /// the unique name of the dispenser
        /// </summary>
        public string DispenserName { get; set; }

        /// <summary>
        /// the unique iot hub registration of the hub
        /// </summary>
        public string DispenserIotId { get; set; }

        /// <summary>
        /// List of all available tools
        /// </summary>
        public Guid DispenserItemId { get; set; }

        /// <summary>
        /// last time the dispenser was checked
        /// </summary>
        public DateTime LastMaintenanceCheck { get; set; }

        /// <summary>
        /// the date the dispenser is created
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// the date the dispenser is scheduled to be decommisioned
        /// </summary>
        public DateTime DecommissionDate { get; set; }

        /// <summary>
        /// the location of the dispenser
        /// </summary>
        public Guid DispenserAddressId { get; set; }
    }
}
