using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;
using Xunit;

namespace ToolShed.Repository.Tests
{
    public class TenantRepositoryTests
    {
        private readonly Tenant tenant;
        private readonly TenantRepository tenantRepository;

        public TenantRepositoryTests()
        {
            tenant = CreateTenant();
            tenantRepository = GetInMemoryTenantRepository();
        }

        [Fact]
        public async Task AddTenantAsync()
        {
            await tenantRepository.AddTenantAsync(tenant);
        }

        [Fact]
        public async Task GetTenantByTenantsId()
        {
            await tenantRepository.AddTenantAsync(tenant);
            var dtoTenant = await tenantRepository.GetTenantByIdAsync(tenant.TenantId);

            Assert.Equal(tenant, dtoTenant);
        }

        [Fact]
        public async Task GetTenantByAddressId()
        {
            await tenantRepository.AddTenantAsync(tenant);
            var dtoTenant = await tenantRepository.GetTenantByAddressIdAsync(tenant.AddressId);

            Assert.Equal(tenant, dtoTenant);
        }

        private Tenant CreateTenant()
        {
            return new Tenant
            {
                TenantId = Guid.NewGuid(),
                TenantName = "MicroBucks",
                AddressId = Guid.NewGuid()
            };
        }

        private TenantRepository GetInMemoryTenantRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new TenantRepository(toolshedContext);
        }
    }
}
