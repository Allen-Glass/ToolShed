using System.Collections.Generic;
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

        public async Task AddAsync(MaintenanceProvider maintenanceProvider)
        {
            await toolShedContext.MaintenanceProviderSet
                .AddAsync(maintenanceProvider);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(MaintenanceProvider maintenanceProvider)
        {
            toolShedContext.MaintenanceProviderSet
                .Remove(maintenanceProvider);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
 