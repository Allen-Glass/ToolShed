using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Repository
{
    /// <summary>
    /// Payment associated with a rental or a sale
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// pk of payment
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PaymentId { get; set; }

        /// <summary>
        /// amount of money the client receives
        /// </summary>
        public double ClientPay { get; set; }

        /// <summary>
        /// the percentage of money the client receives
        /// </summary>
        public double ClientPercentage { get; set; }

        /// <summary>
        /// The amount of money ToolShed receives
        /// </summary>
        public double ToolShedPay { get; set; }

        /// <summary>
        /// base value for a rental or purchase
        /// </summary>
        public double BaseRentalFee { get; set; }

        /// <summary>
        /// the sales tax of the payment
        /// </summary>
        public double SalesTaxCost { get; set; }

        /// <summary>
        /// the cost of rental or purchase before sales tax is added
        /// </summary>
        public double PreTaxTotalCost { get; set; }

        /// <summary>
        /// the total cost for the user
        /// </summary>
        public double TotalCost { get; set; }

        /// <summary>
        /// tenant
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// Has tenant been paid yet
        /// </summary>
        public bool TenantHasBeenPaid { get; set; }

        /// <summary>
        /// how the user is paying for the transaction
        /// </summary>
        public PaymentType PaymentType { get; set; }
    }
}
