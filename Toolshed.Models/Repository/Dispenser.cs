using System;
using System.Collections.Generic;
using Toolshed.Models.Tools;

namespace Toolshed.Models.Repository
{
    /// <summary>
    /// Tool dispenser object
    /// </summary>
    public class Dispenser
    {
        /// <summary>
        /// pk of dispenser id
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// the unique name of the dispenser
        /// </summary>
        public string DispenserName { get; set; }

        /// <summary>
        /// List of all available tools
        /// </summary>
        public Guid DispenserToolsListId { get; set; }

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
        /// list of dispensable items
        /// </summary>
        public IEnumerable<Dispensibles> Dispensibles { get; set; }

        /// <summary>
        /// the location of the dispenser
        /// </summary>
        public Address DispenserLocation { get; set; }
    }
}
