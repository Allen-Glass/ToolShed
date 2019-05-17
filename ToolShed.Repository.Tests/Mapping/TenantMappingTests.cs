using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.Repository;
using ToolShed.Repository.Mapping;
using Xunit;

namespace ToolShed.Repository.Tests.Mapping
{
    public class TenantMappingTests
    {
        private readonly Models.API.Tenant tenant;
        private readonly Tenant dtoTenant;
        private Guid tenantId = Guid.NewGuid();

        public TenantMappingTests()
        {
            tenant = CreateTenant();
            dtoTenant = CreateDtoTenantWithId();
        }

        [Fact]
        public void CreateDtoTenant()
        {
            var dtoTenant = TenantMapping.CreateDtoTenant(tenant, Guid.NewGuid());
            
            Assert.NotNull(dtoTenant.TenantName);
        }

        [Fact]
        public void ConvertDtoTenantToTenant()
        {
            var tenant = TenantMapping.ConvertDtoTenantToTenant(dtoTenant);

            Assert.Equal(tenantId, tenant.TenantId);
            Assert.NotNull(tenant.TenantName);
        }

        private Models.API.Tenant CreateTenant()
        {
            return new Models.API.Tenant
            {
                TenantName = "MicroBucks",
                Address = new Models.API.Address()
            };
        }

        private Tenant CreateDtoTenantWithId()
        {
            return new Tenant
            {
                TenantId = tenantId,
                TenantName = "MicroBucks"
            };
        }
    }
}
