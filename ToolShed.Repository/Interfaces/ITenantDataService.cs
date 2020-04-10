using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface ITenantDataService
    {
        /// <summary>
        /// store tenant information in sql
        /// </summary>
        /// <param name="tenant">tenant object</param>
        Task StoreTenantAsync(Tenant tenant, CancellationToken cancellationToken = default);

        /// <summary>
        /// add user to tenant
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddUserToTenantAsync(Tenant tenant, User user, CancellationToken cancellationToken = default);

        /// <summary>
        /// get tenant by their id
        /// </summary>
        /// <param name="tenantId">tenant pk</param>
        /// <returns>tenant object</returns>
        Task<Tenant> GetTenantAsync(Guid tenantId, CancellationToken cancellationToken = default);

        /// <summary>
        /// get all tenants
        /// </summary>
        /// <returns>list of tenants</returns>
        Task<IEnumerable<Tenant>> GetTenantsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// get list of tenants by their ids
        /// </summary>
        /// <param name="tenantIds">list of tenant ids</param>
        /// <returns>list of tenants</returns>
        Task<IEnumerable<Tenant>> GetTenantsAsync(IEnumerable<Guid> tenantIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// delete tenant
        /// </summary>
        /// <param name="tenant">tenant object</param>
        Task DeleteTenantAsync(Tenant tenant, CancellationToken cancellationToken = default);

        /// <summary>
        /// delete tenant by tenant id
        /// </summary>
        /// <param name="tenantId">pk of tenant</param>
        Task DeleteTenantAsync(Guid tenantId, CancellationToken cancellationToken = default);
    }
}
