using System;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Payments.Interfaces;
using ToolShed.Repository.Interfaces;

namespace ToolShed.Payments
{
    public class TaxService : ITaxService
    {
        private readonly ITaxesSQLService taxesSQLService;

        public TaxService(ITaxesSQLService taxesSQLService)
        {
            this.taxesSQLService = taxesSQLService;
        }

        public async Task<Payment> AppendSalesTaxAsync(Payment payment, string state)
        {
            if (payment == null)
                throw new ArgumentNullException();

            var salesTax = await taxesSQLService.GetStateSalesTaxAsync(state);
            payment.SalesTaxCost = (payment.PreTaxTotalCost * salesTax);
            payment.TotalCost = payment.SalesTaxCost + payment.PreTaxTotalCost;

            return payment;
        }

        public async Task<double> GetSalesTaxAsync(Payment payment, string state)
        {
            if (payment == null)
                throw new ArgumentNullException();

            return await taxesSQLService.GetStateSalesTaxAsync(state);
        }
    }
}
