using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Context;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;
using ToolShed.Repository.Services;
using Xunit;

namespace ToolShed.Repository.Tests.Services
{
    public class TenantDataServiceTests
    {
        private readonly ITenantDataService tenantDataService;
        private readonly Tenant tenant;

        public TenantDataServiceTests()
        {
            tenantDataService = CreateTenantDataService();
            tenant = CreateTenant();
        }

        [Fact]
        public async Task StoreTenantAsync()
        {
            await tenantDataService.StoreTenantAsync(tenant);
        }

        [Fact]
        public async Task AddUserToTenantAsync()
        {
            var user = new User
            {
                FirstName = "John"
            };
            tenant.TenantId = Guid.NewGuid();

            await tenantDataService.AddUserToTenantAsync(tenant, user);
        }

        [Fact]
        public async Task GetTenantAsync()
        {
            this.tenant.TenantId = Guid.NewGuid();

            var tenant = await tenantDataService.GetTenantAsync(this.tenant.TenantId);
        }

        private Tenant CreateTenant()
        {
            return new Tenant
            {
                TenantName = "MicroBucks",
                Address = new Address()
            };
        }

        private ITenantDataService CreateTenantDataService()
        {
            return new TenantDataService(GetInMemoryTenantRepository(),
                GetInMemoryTenantUserRepository(),
                GetInMemoryAddressRepository(),
                GetInMemoryUserRepository());
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

        private TenantUserRepository GetInMemoryTenantUserRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new TenantUserRepository(toolshedContext);
        }

        private AddressRepository GetInMemoryAddressRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new AddressRepository(toolshedContext);
        }

        private UserRepository GetInMemoryUserRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new UserRepository(toolshedContext);
        }
    }
}
