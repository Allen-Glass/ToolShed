using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using ToolShed.Helpers;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    /// <summary>
    /// storing tenant information
    /// </summary>
    public class TenantDataService : ITenantDataService
    {
        private readonly TenantRepository tenantRepository;
        private readonly TenantUserRepository tenantUserRepository;
        private readonly AddressRepository addressRepository;
        private readonly UserRepository userRepository;

        public TenantDataService(TenantRepository tenantRepository,
            AddressRepository addressRepository,
            TenantUserRepository tenantUserRepository,
            UserRepository userRepository)
        {
            this.tenantRepository = tenantRepository;
            this.addressRepository = addressRepository;
            this.tenantUserRepository = tenantUserRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// store tenant infomation
        /// </summary>
        /// <param name="tenant">tenant object</param>
        public async Task StoreTenantAsync(Tenant tenant)
        {
            if (tenant == null)
                throw new ArgumentNullException();

            var addressId = await addressRepository.AddAddressAsync(AddressMapping.CreateDtoAddress(tenant.Address));
            await tenantRepository.AddTenantAsync(TenantMapping.CreateDtoTenant(tenant, addressId));
        }

        public async Task AddUserToTenantAsync(Tenant tenant, User user)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(tenant);
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(user);

            await tenantUserRepository.AddUserToTenantAsync(tenant.TenantId, user.UserId);
        }

        public async Task<Tenant> GetTenantAsync(Guid tenantId)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoTenant = await tenantRepository.GetTenantByIdAsync(tenantId);
            var dtoAddress = await addressRepository.GetAddressAsync(dtoTenant.AddressId);

            if (dtoTenant == null || dtoAddress == null)
                throw new NullReferenceException();

            var tenant = TenantMapping.ConvertDtoTenantToTenant(dtoTenant);
            tenant.Address = AddressMapping.ConvertDtoAddressToAddress(dtoAddress);

            return tenant;
        }

        public async Task<IEnumerable<User>> GetAllUsersInTenantAsync(Guid tenantId)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(tenantId);

            var userIds = await tenantUserRepository.GetAllUserIdsInTenantAsync(tenantId);
            var users = await userRepository.GetUsersAsync(userIds);

            return UserMapping.ConvertDtoUsers(users);
        }

        /// <summary>
        /// Get all tenants
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Tenant>> GetTenantsAsync()
        {
            var dtoTenants = await tenantRepository.GetAllTenantsAsync();

            if (dtoTenants == null)
                throw new NullReferenceException();

            var tenants = await MapAddressesToTenants(dtoTenants);

            return tenants;
        }

        public async Task<IEnumerable<Tenant>> GetTenantsAsync(Guid userId)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(userId);

            var tenantIds = await tenantUserRepository.GetAllTenantIdsForUserAsync(userId);
            var dtoTenants = await tenantRepository.GetTenantsByTenantIdsAsync(tenantIds);

            return TenantMapping.ConvertDtoTenantsToTenants(dtoTenants);
        }

        /// <summary>
        /// Get all tenants by their id
        /// </summary>
        /// <param name="tenantIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Tenant>> GetTenantsAsync(IEnumerable<Guid> tenantIds)
        {
            if (tenantIds == null)
                throw new ArgumentNullException();

            var dtoTenants = await tenantRepository.GetTenantsByTenantIdsAsync(tenantIds);
            var tenants = await MapAddressesToTenants(dtoTenants);

            if (dtoTenants == null)
                throw new NullReferenceException();

            return tenants;
        }

        public async Task<bool> IsUserInTenantAsync(Tenant tenant, User user)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(tenant.TenantId);
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(user.UserId);

            return await tenantUserRepository.IsUserInTenantAsync(tenant.TenantId, user.UserId);
        }

        public async Task DeleteTenantAsync(Tenant tenant)
        {

        }

        public async Task DeleteTenantAsync(Guid tenantId)
        {

        }

        /// <summary>
        /// Map addresses to tenants
        /// </summary>
        /// <param name="dtoTenants"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Tenant>> MapAddressesToTenants(IEnumerable<Models.Repository.Tenant> dtoTenants)
        {
            var tenants = new List<Tenant>();

            foreach (var dtoTenant in dtoTenants)
            {
                var address = await addressRepository.GetAddressAsync(dtoTenant.AddressId);
                var tenant = TenantMapping.ConvertDtoTenantToTenant(dtoTenant);
                tenant.Address = AddressMapping.ConvertDtoAddressToAddress(address);
                tenants.Add(tenant);
            }

            return tenants;
        }
    }
}
