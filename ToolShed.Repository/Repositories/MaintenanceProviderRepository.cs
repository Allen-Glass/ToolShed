using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class MaintenanceProviderRepository
    {
        private readonly ToolShedContext toolShedContext;

        public MaintenanceProviderRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(MaintenanceProvider maintenanceProvider, CancellationToken cancellationToken = default)
        {
            await toolShedContext.MaintenanceProviderSet
                .AddAsync(maintenanceProvider, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(MaintenanceProvider maintenanceProvider, CancellationToken cancellationToken = default)
        {
            toolShedContext.MaintenanceProviderSet
                .Remove(maintenanceProvider);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
 