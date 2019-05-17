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
        private readonly ITenantDataService tenantSQLService;

        public TenantService(ITenantDataService tenantSQLService)
        {
            this.tenantSQLService = tenantSQLService;
        }

        public async Task AddTenantAsync(Tenant tenant)
        {
            if (tenant == null)
                throw new ArgumentNullException();

            await tenantSQLService.StoreTenantAsync(tenant);
        }

        public async Task<Tenant> GetTenantAsync(Guid tenantId)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentNullException();

            var tenant = await tenantSQLService.GetTenantAsync(tenantId);

            if (tenant == null)
                throw new NullReferenceException();

            return tenant;
        }
    }
}
