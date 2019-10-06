using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;
using Xunit;

namespace ToolShed.Repository.Tests.Repository
{
    public class TenantUserRepositoryTests
    {
        private readonly Tenant tenant;
        private readonly TenantUserRepository tenantUserRepository;
        private Guid tenantId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();

        public TenantUserRepositoryTests()
        {
            tenant = CreateTenant();
            tenantUserRepository = GetInMemoryTenantRepository();
        }

        [Fact]
        public async Task AddTenantAsync()
        {
            await tenantUserRepository.AddAsync(tenantId, userId);
        }

        [Fact]
        public async Task AddTenantAsync_Object()
        {
            var tenantUser = new TenantUser
            {
                TenantId = tenantId,
                UserId = userId
            };

            await tenantUserRepository.AddAsync(tenantUser);
        }

        [Fact]
        public async Task GetAllUserIdsInTenantAsync()
        {
            await tenantUserRepository.AddAsync(tenantId, userId);
            var userIds = await tenantUserRepository.ListAsync(tenantId);

            Assert.Equal(userId, userIds.FirstOrDefault());
        }

        [Fact]
        public async Task GetAllTenantIdsForUserAsync()
        {
            await tenantUserRepository.AddAsync(tenantId, userId);
            var tenantIds = await tenantUserRepository.GetAllTenantIdsForUserAsync(userId);

            Assert.Equal(tenantId, tenantIds.FirstOrDefault());
        }

        [Fact]
        public async Task IsUserInTenantAsync_True()
        {
            await tenantUserRepository.AddAsync(tenantId, userId);
            var isInTenant = await tenantUserRepository.IsUserInTenantAsync(tenantId, userId);

            Assert.True(isInTenant);
        }

        [Fact]
        public async Task IsUserInTenantAsync_False()
        {
            var newUserId = Guid.NewGuid();
            await tenantUserRepository.AddAsync(tenantId, userId);
            var isInTenant = await tenantUserRepository.IsUserInTenantAsync(tenantId, newUserId);

            Assert.False(isInTenant);
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

        private TenantUserRepository GetInMemoryTenantRepository()
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
    }
}
