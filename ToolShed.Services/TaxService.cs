using System;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Services.Interfaces;

namespace ToolShed.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxesDataService taxesSQLService;

        public TaxService(ITaxesDataService taxesSQLService)
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
