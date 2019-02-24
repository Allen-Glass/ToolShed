using System;
using Toolshed.Models.Enums;

namespace Toolshed.Models.SQL
{
    public class Tool
    {
        /// <summary>
        /// pk of tool
        /// </summary>
        public Guid ToolId { get; set; }

        /// <summary>
        /// Dispenser that the tool is being associated with
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// part number
        /// </summary>
        public PartNumber ToolType { get; set; }

        /// <summary>
        /// tool needs repair
        /// </summary>
        public bool NeedsRepair { get; set; }

        /// <summary>
        /// tool needs replacement
        /// </summary>
        public bool NeedsReplacement { get; set; }

        /// <summary>
        /// Tool is available to be used
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// date when tool is first accounted for in logistics cycle
        /// </summary>
        public DateTime AccountedForDate { get; set; }

        /// <summary>
        /// Date of last inspection
        /// </summary>
        public DateTime LastInspection { get; set; }

        /// <summary>
        /// date when tool is to be decommissioned
        /// </summary>
        public DateTime DecommissionDate { get; set; }
    }
}
