using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task AddStateSalesTaxAsync(StateSalesTaxes stateSalesTaxes)
        {
            if (stateSalesTaxes == null)
                throw new ArgumentNullException();

            await toolShedContext.StateSalesTaxesSet
                .AddAsync(stateSalesTaxes);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<StateSalesTaxes> GetStateSalesTaxAsync(string state)
        {
            if (string.IsNullOrEmpty(state))
                throw new ArgumentNullException();

            var dtoStateSalesTax = await toolShedContext.StateSalesTaxesSet
                .FirstOrDefaultAsync(c => c.State.Equals(state));

            if (dtoStateSalesTax == null)
                throw new NullReferenceException();

            return dtoStateSalesTax;
        }

        public async Task<StateSalesTaxes> GetStateSalesTaxAsync(Guid stateSalesTaxesId)
        {
            if (stateSalesTaxesId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoStateSalesTax = await toolShedContext.StateSalesTaxesSet
                .FirstOrDefaultAsync(c => c.StateSalesTaxesId.Equals(stateSalesTaxesId));

            if (dtoStateSalesTax == null)
                throw new NullReferenceException();

            return dtoStateSalesTax;
        }
    }
}
