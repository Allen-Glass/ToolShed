using Toolshed.Models.Enums;

namespace Toolshed.Models.Tools
{
    /// <summary>
    /// Item for screws, nails, and other smaller items not meant to be returned
    /// </summary>
    public class Dispensibles
    {
        /// <summary>
        /// Text name of object being dispensed
        /// </summary>
        public string DispensibleName { get; set; }

        /// <summary>
        /// Identifier of dispensible item
        /// </summary>
        public PartNumber ToolType { get; set; }

        /// <summary>
        /// The number of items being dispensed
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// the cost per single item
        /// </summary>
        public double PricePerUnit { get; set; }
    }
}
