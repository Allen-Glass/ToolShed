using ToolShed.Models.Enums;

namespace ToolShed.Models.API
{
    /// <summary>
    /// Payment associated with a rental or a sale
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// amount of money the client receives
        /// </summary>
        public double ClientPay { get; set; }

        /// <summary>
        /// the percentage of money ToolShed receives
        /// </summary>
        public double ToolShedPercentage { get; set; }

        /// <summary>
        /// The amount of money ToolShed receives
        /// </summary>
        public double ToolShedPay { get; set; }

        /// <summary>
        /// cost before sales, taxes, etc...
        /// </summary>
        public double BaseCost { get; set; }

        /// <summary>
        /// the total cost for the user
        /// </summary>
        public double TotalCost { get; set; }

        /// <summary>
        /// how the user is paying for the transaction
        /// </summary>
        public PaymentType PaymentType { get; set; }
    }
}
