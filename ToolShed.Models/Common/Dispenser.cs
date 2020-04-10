using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.API
{
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
        /// the unique iot hub registration of the hub
        /// </summary>
        public string DispenserIotName { get; set; }

        /// <summary>
        /// List of all available tools
        /// </summary>
        public IEnumerable<Item> AvailableItems { get; set; }

        /// <summary>
        /// last time the dispenser was checked
        /// </summary>
        public DateTime LastMaintenanceCheckDate { get; set; }

        /// <summary>
        /// the date the dispenser is created
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// the date the dispenser is scheduled to be decommisioned
        /// </summary>
        public DateTime DecommishDate { get; set; }

        /// <summary>
        /// the location of the dispenser
        /// </summary>
        public Address DispenserAddress { get; set; }
    }
}
