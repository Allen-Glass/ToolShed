using System;

namespace ToolShed.Models.Repository
{
    public class StateSalesTaxes
    {
        public Guid StateSalesTaxesId { get; set; }

        public string State { get; set; }

        public double SalesTaxPercentage { get; set; }
    }
}
