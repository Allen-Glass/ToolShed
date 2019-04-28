using System;
using Toolshed.Models.Enums;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class TenantMapping
    {
        public static Models.Repository.Address CreateDtoAddress(Address address)
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

        public static Address ConvertDtoAddressToAddress(Models.Repository.Address address)
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

        public static Models.Repository.Tenant CreateDtoTenant(Tenant tenant, Guid addressId)
        {
            return new Models.Repository.Tenant
            {
                AddressId = addressId,
                TenantName = tenant.TenantName
            };
        }

        public static Tenant ConvertDtoTenantToTenant(Models.Repository.Tenant tenant, Address address)
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
