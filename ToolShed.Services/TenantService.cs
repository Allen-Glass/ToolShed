using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Services.Interfaces;

namespace ToolShed.Services
{
    public class TenantService
    {
        private readonly ITenantDataService tenantSQLService;
        private readonly IEmailService emailService;

        public TenantService(ITenantDataService tenantSQLService,
            IEmailService emailService)
        {
            this.tenantSQLService = tenantSQLService;
            this.emailService = emailService;
        }

        public async Task AddTenantAsync(Tenant tenant)
        {
            if (tenant == null)
                throw new ArgumentNullException(nameof(tenant));

            await tenantSQLService.StoreTenantAsync(tenant);
        }

        public async Task InviteUserToTenantAsync(Tenant tenant, User user)
        {
            if (tenant == null)
                throw new ArgumentNullException(nameof(tenant));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await emailService.SendUserInviteToTenantAsync(user.Email);
        }

        public async Task AcceptUserInviteAsync(Tenant tenant, User user)
        {
            if (tenant == null)
                throw new ArgumentNullException(nameof(tenant));

            if (user == null)
                throw new ArgumentNullException(nameof(user));
        }

        public async Task<Tenant> GetTenantAsync(Guid tenantId)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentNullException(nameof(tenantId));

            var tenant = await tenantSQLService.GetTenantAsync(tenantId);

            if (tenant == null)
                throw new NullReferenceException(); //create sql referance not found exception

            return tenant;
        }
    }
}
