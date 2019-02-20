using System;
using Toolshed.Models.Enums;

namespace Toolshed.Models.Tools
{
    /// <summary>
    /// The tool to be or has been rented objected
    /// </summary>
    public class Tool
    {
        /// <summary>
        /// pk of tool
        /// </summary>
        public Guid ToolId { get; set; }

        /// <summary>
        /// Display name of tool
        /// </summary>
        public string ToolName { get; set; }

        /// <summary>
        /// unique identifier of tool type
        /// </summary>
        public PartNumber ToolType { get; set; }

        /// <summary>
        /// The parent dispenser
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// Tool availability
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Tool health
        /// </summary>
        public bool NeedsRepair { get; set; }

        /// <summary>
        /// tool needs inspection
        /// </summary>
        public bool NeedsInspection { get; set; }

        /// <summary>
        /// Tool not reported on
        /// </summary>
        public bool IsMissing { get; set; }
    }
}
