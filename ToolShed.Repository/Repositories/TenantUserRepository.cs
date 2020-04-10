using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task AddAsync(Guid tenantId, Guid userId, CancellationToken cancellationToken = default)
        {
            var tenantUser = new TenantUser
            {
                TenantId = tenantId,
                UserId = userId
            };

            await toolShedContext.TenantUserSet
                .AddAsync(tenantUser);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAsync(TenantUser tenantUser, CancellationToken cancellationToken = default)
        {
            await toolShedContext.TenantUserSet
                .AddAsync(tenantUser);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> ListAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.TenantUserSet
                .Where(c => c.TenantId.Equals(tenantId))
                .Select(c => c.UserId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> GetAllTenantIdsForUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.TenantUserSet
                .Where(c => c.UserId.Equals(userId))
                .Select(c => c.TenantId)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsUserInTenantAsync(Guid tenantId, Guid userId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.TenantUserSet
                .Where(c => c.TenantId.Equals(tenantId))
                .AnyAsync(c => c.UserId.Equals(userId));
        }

        public async Task DeleteTenantAsync(Guid tenantId, Guid userId, CancellationToken cancellationToken = default)
        {
            var tenantUser = new TenantUser
            {
                TenantId = tenantId,
                UserId = userId
            };

            toolShedContext.Remove(tenantUser);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteUserFromTenantAsync(TenantUser tenantUser, CancellationToken cancellationToken = default)
        {
            toolShedContext.Remove(tenantUser);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
