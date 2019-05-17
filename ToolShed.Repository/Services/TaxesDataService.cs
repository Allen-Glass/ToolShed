using System;
using System.Threading.Tasks;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class TaxesDataService : ITaxesDataService
    {
        private readonly StateSalesTaxRepository stateSalesTaxRepository;

        public TaxesDataService(StateSalesTaxRepository stateSalesTaxRepository)
        {
            this.stateSalesTaxRepository = stateSalesTaxRepository;
        }

        public async Task<double> GetStateSalesTaxAsync(string state)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException();

            var stateSalesTax = await stateSalesTaxRepository.GetStateSalesTaxAsync(state);

            return stateSalesTax.SalesTaxPercentage;
        }
    }
}
