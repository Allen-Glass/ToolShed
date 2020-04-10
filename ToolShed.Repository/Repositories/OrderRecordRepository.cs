using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class OrderRecordRepository
    {
        private readonly ToolShedContext toolShedContext;

        public OrderRecordRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext ?? throw new ArgumentNullException(nameof(toolShedContext));
        }

        public async Task AddAsync(OrderRecord orderRecord, CancellationToken cancellationToken = default)
        {
            if (orderRecord == null)
                throw new ArgumentNullException(nameof(orderRecord));

            await toolShedContext.AddAsync(orderRecord, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task GetAsync(CancellationToken cancellationToken = default)
        {

        }

        public async Task ListAsync(CancellationToken cancellationToken = default)
        {

        }
    }
}
