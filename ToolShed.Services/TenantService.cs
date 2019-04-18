using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;

namespace ToolShed.Services
{
    public class TenantService
    {
        private readonly ITenantSQLService tenantSQLService;

        public TenantService(ITenantSQLService tenantSQLService)
        {
            this.tenantSQLService = tenantSQLService;
        }

        public async Task AddTenantAsync(Tenant tenant)
        {
            if (tenant == null)
                throw new ArgumentNullException();

            await tenantSQLService.StoreTenantAsync(tenant);
        }

        public async Task 
    }
}
