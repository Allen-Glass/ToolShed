using System;
using System.Threading;
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
            this.stateSalesTaxRepository = stateSalesTaxRepository ?? throw new ArgumentNullException(nameof(stateSalesTaxRepository));
        }

        public async Task<double> GetStateSalesTaxAsync(string state, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException(nameof(state));

            var stateSalesTax = await stateSalesTaxRepository.GetAsync(state, cancellationToken);

            return stateSalesTax.SalesTaxPercentage;
        }
    }
}
