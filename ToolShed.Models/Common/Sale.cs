namespace ToolShed.Models.API
{
    /// <summary>
    /// sale discount of an item
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// marks active sale
        /// </summary>
        public double IsActive { get; set; }

        /// <summary>
        /// The whole value off an item
        /// </summary>
        public double DiscountPrice { get; set; }

        /// <summary>
        /// The percentage the item is off
        /// </summary>
        public double DiscountPercentage { get; set; }
    }
}
