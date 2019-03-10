using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface ITenantSQLService
    {
        /// <summary>
        /// store tenant information in sql
        /// </summary>
        /// <param name="tenant">tenant object</param>
        Task StoreTenantAsync(Tenant tenant);

        /// <summary>
        /// get tenant by their id
        /// </summary>
        /// <param name="tenantId">tenant pk</param>
        /// <returns>tenant object</returns>
        Task<Tenant> GetTenantAsync(Guid tenantId);

        /// <summary>
        /// get all tenants
        /// </summary>
        /// <returns>list of tenants</returns>
        Task<IEnumerable<Tenant>> GetTenantsAsync();

        /// <summary>
        /// get list of tenants by their ids
        /// </summary>
        /// <param name="tenantIds">list of tenant ids</param>
        /// <returns>list of tenants</returns>
        Task<IEnumerable<Tenant>> GetTenantsAsync(IEnumerable<Guid> tenantIds);

        /// <summary>
        /// delete tenant
        /// </summary>
        /// <param name="tenant">tenant object</param>
        Task DeleteTenantAsync(Tenant tenant);

        /// <summary>
        /// delete tenant by tenant id
        /// </summary>
        /// <param name="tenantId">pk of tenant</param>
        Task DeleteTenantAsync(Guid tenantId);
    }
}
