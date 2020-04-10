using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class StateSalesTaxRepository
    {
        private readonly ToolShedContext toolShedContext;

        public StateSalesTaxRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(StateSalesTaxes stateSalesTaxes, CancellationToken cancellationToken = default)
        {
            if (stateSalesTaxes == null)
                throw new ArgumentNullException();

            await toolShedContext.StateSalesTaxesSet
                .AddAsync(stateSalesTaxes, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<StateSalesTaxes> GetAsync(string state, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException();

            var dtoStateSalesTax = await toolShedContext.StateSalesTaxesSet
                .FirstOrDefaultAsync(c => c.State.Equals(state), cancellationToken);

            if (dtoStateSalesTax == null)
                throw new NullReferenceException();

            return dtoStateSalesTax;
        }

        public async Task<StateSalesTaxes> GetAsync(Guid stateSalesTaxesId, CancellationToken cancellationToken = default)
        {
            if (stateSalesTaxesId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoStateSalesTax = await toolShedContext.StateSalesTaxesSet
                .FirstOrDefaultAsync(c => c.StateSalesTaxesId.Equals(stateSalesTaxesId), cancellationToken);

            if (dtoStateSalesTax == null)
                throw new NullReferenceException();

            return dtoStateSalesTax;
        }
    }
}
