using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    /// <summary>
    /// storing tenant information
    /// </summary>
    public class TenantSQLService : ITenantSQLService
    {
        private readonly TenantRepository tenantRepository;
        private readonly AddressRepository addressRepository;

        public TenantSQLService(TenantRepository tenantRepository
            , AddressRepository addressRepository)
        {
            this.tenantRepository = tenantRepository;
            this.addressRepository = addressRepository;
        }

        /// <summary>
        /// store tenant infomation
        /// </summary>
        /// <param name="tenant">tenant object</param>
        public async Task StoreTenantAsync(Tenant tenant)
        {
            if (tenant == null)
                throw new ArgumentNullException();

            var addressId = await addressRepository.AddAddressAsync(CreateDtoAddress(tenant.Address));
            await tenantRepository.AddTenantAsync(CreateDtoTenant(tenant, addressId));
        }

        public async Task<Tenant> GetTenantAsync(Guid tenantId)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoTenant = await tenantRepository.GetTenantByIdAsync(tenantId);
            var dtoAddress = await addressRepository.GetAddressByAddressIdAsync(dtoTenant.TenantAddressId);

            if (dtoTenant == null || dtoAddress == null)
                throw new NullReferenceException();

            var tenant = ConvertDtoTenantToTenant(dtoTenant, ConvertDtoAddressToAddress(dtoAddress));

            return tenant;
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
                var address = await addressRepository.GetAddressByAddressIdAsync(dtoTenant.TenantAddressId);
                tenants.Add(ConvertDtoTenantToTenant(dtoTenant, ConvertDtoAddressToAddress(address)));
            }

            return tenants;
        }

        private Models.Repository.Address CreateDtoAddress(Address address)
        {
            return new Models.Repository.Address
            {
                AddressType = AddressType.Tenant,
                AptNumber = address.AptNumber,
                City = address.City,
                Country = address.Country,
                State = address.State,
                StreetName = address.StreetName,
                StreetName2 = address.StreetName2,
                ZipCode = address.ZipCode
            };
        }

        private Address ConvertDtoAddressToAddress(Models.Repository.Address address)
        {
            return new Address
            {
                AddressId = address.AddressId,
                AptNumber = address.AptNumber,
                City = address.City,
                Country = address.Country,
                State = address.State,
                StreetName = address.StreetName,
                StreetName2 = address.StreetName2,
                ZipCode = address.ZipCode
            };
        }

        private Models.Repository.Tenant CreateDtoTenant(Tenant tenant, Guid addressId)
        {
            return new Models.Repository.Tenant
            {
                TenantAddressId = addressId,
                TenantName = tenant.TenantName
            };
        }

        private Tenant ConvertDtoTenantToTenant(Models.Repository.Tenant tenant, Address address)
        {
            return new Tenant
            {
                TenantId = tenant.TenantId,
                TenantName = tenant.TenantName,
                Address = address
            };
        }
    }
}
