using System;
using System.Collections.Generic;
using Toolshed.Models.Tools;

namespace Toolshed.Models.Dispensers
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
        /// List of all available tools
        /// </summary>
        public IEnumerable<Tool> AvailableTools { get; set; }

        /// <summary>
        /// last time the dispenser was checked
        /// </summary>
        public DateTime LastMaintenanceCheck { get; set; }

        /// <summary>
        /// list of dispensable items
        /// </summary>
        public IEnumerable<Dispensibles> Dispensibles { get; set; }
    }
}
