using System.Collections.Generic;
using Toolshed.Models.Enums;

namespace Toolshed.Models.Tools
{
    /// <summary>
    /// The tool set object
    /// </summary>
    public class ToolSet
    {
        /// <summary>
        /// Toolset display name
        /// </summary>
        public string ToolSetName { get; set; }

        /// <summary>
        /// Unique identifier of toolset
        /// </summary>
        public ToolType ToolType { get; set; }

        /// <summary>
        /// parts that are missing in the set
        /// </summary>
        public IEnumerable<Tool> MissingParts { get; set; }

        /// <summary>
        /// Is the tool set avaible
        /// </summary>
        public bool IsAvailable { get; set; }
        
        /// <summary>
        /// tool set reported as needing repairing
        /// </summary>
        public bool NeedsRepair { get; set; }

        /// <summary>
        /// tool set reported as missing
        /// </summary>
        public bool IsMissing { get; set; }
    }
}
