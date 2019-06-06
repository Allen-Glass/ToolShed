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
    public class TenantUserRepository
    {
        private readonly ToolShedContext toolShedContext;

        public TenantUserRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddUserToTenantAsync(Guid tenantId, Guid userId)
        {
            var tenantUser = new TenantUser
            {
                TenantId = tenantId,
                UserId = userId
            };

            await toolShedContext.TenantUserSet
                .AddAsync(tenantUser);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task AddUserToTenantAsync(TenantUser tenantUser)
        {
            await toolShedContext.TenantUserSet
                .AddAsync(tenantUser);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guid>> GetAllUserIdsInTenantAsync(Guid tenantId)
        {
            return await toolShedContext.TenantUserSet
                .Where(c => c.UserId.Equals(tenantId))
                .Select(c => c.TenantId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Guid>> GetAllTenantIdsForUserAsync(Guid userId)
        {
            return await toolShedContext.TenantUserSet
                .Where(c => c.TenantId.Equals(userId))
                .Select(c => c.UserId)
                .ToListAsync();
        }

        public async Task<bool> IsUserInTenantAsync(Guid tenantId, Guid userId)
        {
            return await toolShedContext.TenantUserSet
                .Where(c => c.TenantId.Equals(tenantId))
                .AnyAsync(c => c.UserId.Equals(userId));
        }

        public async Task RemoveUserFromTenant(Guid tenantId, Guid userId)
        {
            var tenantUser = new TenantUser
            {
                TenantId = tenantId,
                UserId = userId
            };

            toolShedContext.Remove(tenantUser);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task RemoveUserFromTenant(TenantUser tenantUser)
        {
            toolShedContext.Remove(tenantUser);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
