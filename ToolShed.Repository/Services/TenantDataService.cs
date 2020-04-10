using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Helpers;
using ToolShed.Models.API;
using ToolShed.Models.Exceptions;
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
            TenantUserRepository tenantUserRepository,
            AddressRepository addressRepository,
            UserRepository userRepository)
        {
            this.tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
            this.addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            this.tenantUserRepository = tenantUserRepository;
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// store tenant infomation
        /// </summary>
        /// <param name="tenant">tenant object</param>
        public async Task StoreTenantAsync(Tenant tenant, CancellationToken cancellationToken = default)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(tenant);

            var addressId = await addressRepository.AddAsync(tenant.Address.CreateDtoAddress());
            await tenantRepository.AddAsync(tenant.CreateDtoTenant(addressId), cancellationToken);
        }

        public async Task AddUserToTenantAsync(Tenant tenant, User user, CancellationToken cancellationToken = default)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(tenant);
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(user);

            await tenantUserRepository.AddAsync(tenant.TenantId, user.UserId);
        }

        public async Task<Tenant> GetTenantAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(tenantId);

            var dtoTenant = await tenantRepository.GetAsync(tenantId, cancellationToken);
            var dtoAddress = await addressRepository.GetAsync(dtoTenant.AddressId, cancellationToken);

            if (dtoAddress == null)
                throw new SqlEntityNullReferenceException(nameof(dtoAddress), dtoAddress.AddressId.ToString());

            if (dtoTenant == null)
                throw new SqlEntityNullReferenceException(nameof(dtoAddress), dtoTenant.TenantId.ToString());

            var tenant = TenantMapping.ConvertDtoTenantToTenant(dtoTenant);
            tenant.Address = AddressMapping.ConvertDtoAddressToAddress(dtoAddress);

            return tenant;
        }

        public async Task<IEnumerable<User>> GetAllUsersInTenantAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(tenantId);

            var userIds = await tenantUserRepository.ListAsync(tenantId, cancellationToken);
            var users = await userRepository.GetUsersAsync(userIds, cancellationToken);

            return UserMapping.ConvertDtoUsers(users);
        }

        /// <summary>
        /// Get all tenants
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Tenant>> GetTenantsAsync(CancellationToken cancellationToken = default)
        {
            var dtoTenants = await tenantRepository.ListAsync(cancellationToken);

            if (dtoTenants == null)
                throw new SqlEntityNullReferenceException(nameof(dtoTenants), nameof(dtoTenants)); //add argument to this exception for lists

            var tenants = await MapAddressesToTenants(dtoTenants);

            return tenants;
        }

        public async Task<IEnumerable<Tenant>> GetTenantsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(userId);

            var tenantIds = await tenantUserRepository.GetAllTenantIdsForUserAsync(userId, cancellationToken);
            var dtoTenants = await tenantRepository.ListAsync(tenantIds, cancellationToken);

            return dtoTenants.ConvertDtoTenantsToTenants();
        }

        /// <summary>
        /// Get all tenants by their id
        /// </summary>
        /// <param name="tenantIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Tenant>> GetTenantsAsync(IEnumerable<Guid> tenantIds, CancellationToken cancellationToken = default)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(tenantIds);

            var dtoTenants = await tenantRepository.ListAsync(tenantIds);
            var tenants = await MapAddressesToTenants(dtoTenants);

            if (dtoTenants == null)
                throw new SqlEntityNullReferenceException(nameof(dtoTenants), nameof(tenantIds));

            return tenants;
        }

        public async Task<bool> IsUserInTenantAsync(Tenant tenant, User user, CancellationToken cancellationToken = default)
        {
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(tenant.TenantId);
            NullCheckHelpers.EnsureArgumentIsNotNullOrEmpty(user.UserId);

            return await tenantUserRepository.IsUserInTenantAsync(tenant.TenantId, user.UserId, cancellationToken);
        }

        public async Task DeleteTenantAsync(Tenant tenant, CancellationToken cancellationToken = default)
        {

        }

        public async Task DeleteTenantAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {

        }

        /// <summary>
        /// Map addresses to tenants
        /// </summary>
        /// <param name="dtoTenants"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Tenant>> MapAddressesToTenants(IEnumerable<Models.Repository.Tenant> dtoTenants, CancellationToken cancellationToken = default)
        {
            var tenants = new List<Tenant>();

            foreach (var dtoTenant in dtoTenants)
            {
                var address = await addressRepository.GetAsync(dtoTenant.AddressId, cancellationToken);
                var tenant = dtoTenant.ConvertDtoTenantToTenant();
                tenant.Address = address.ConvertDtoAddressToAddress();
                tenants.Add(tenant);
            }

            return tenants;
        }
    }
}
