using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    /// <summary>
    /// Tenant class that interacts directly with sql
    /// </summary>
    public class TenantRepository
    {
        private readonly ToolShedContext toolShedContext;

        public TenantRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        /// <summary>
        /// add tenant
        /// </summary>
        /// <param name="tenant">tenant dto</param>
        public async Task AddTenantAsync(Tenant tenant)
        {
            await toolShedContext.TenantSet
                .AddAsync(tenant);
            await toolShedContext.SaveChangesAsync();
        }

        /// <summary>
        /// grab tenant by their id
        /// </summary>
        /// <param name="tenantId">tenant id</param
        public async Task<Tenant> GetTenantByIdAsync(Guid tenantId)
        {
            return await toolShedContext.TenantSet
                .FirstOrDefaultAsync(c => c.TenantId.Equals(tenantId));
        }

        /// <summary>
        /// grab tenant by their unique address id
        /// </summary>
        /// <param name="addressId">pk of address</param>
        /// <returns>tenant dto</returns>
        public async Task<Tenant> GetTenantByAddressIdAsync(Guid addressId)
        {
            return await toolShedContext.TenantSet
                .FirstOrDefaultAsync(c => c.AddressId.Equals(addressId));
        }

        /// <summary>
        /// Grab all tenants
        /// </summary>
        /// <returns>list of dto tenants</returns>
        public async Task<IEnumerable<Tenant>> GetAllTenantsAsync()
        {
            return await toolShedContext.TenantSet
                .ToListAsync();
        }

        /// <summary>
        /// get tenants by their ids
        /// </summary>
        /// <param name="tenantIds">pk of tenants</param>
        public async Task<IEnumerable<Tenant>> GetTenantsByTenantIdsAsync(IEnumerable<Guid> tenantIds)
        {
            return await toolShedContext.TenantSet
                .Where(c => tenantIds.Contains(c.TenantId))
                .ToListAsync();
        }

        /// <summary>
        /// get tenants by their addresses
        /// </summary>
        /// <param name="addressIds">pks of addresses</param>
        /// <returns>list of tenant dtos</returns>
        public async Task<IEnumerable<Tenant>> GetTenantsByAddressIdsAsync(IEnumerable<Guid> addressIds)
        {
            return await toolShedContext.TenantSet
                .Where(c => addressIds.Contains(c.AddressId))
                .ToListAsync();
        }

        /// <summary>
        /// update tenant information
        /// </summary>
        /// <param name="tenant">tenant dto</param>
        public async Task UpdateTenantAsync(Tenant tenant)
        {
            toolShedContext.TenantSet
                .Update(tenant);
            await toolShedContext.SaveChangesAsync();
        }

        /// <summary>
        /// remove tenant
        /// </summary>
        /// <param name="tenant">tenant dto</param>
        public async Task DeleteTenantAsync(Tenant tenant)
        {
            toolShedContext.TenantSet.Remove(tenant);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
