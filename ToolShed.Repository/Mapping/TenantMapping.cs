using System;
using Toolshed.Models.Enums;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class TenantMapping
    {
        public static Models.Repository.Tenant CreateDtoTenant(Tenant tenant, Guid addressId)
        {
            return new Models.Repository.Tenant
            {
                AddressId = addressId,
                TenantName = tenant.TenantName
            };
        }

        public static Tenant ConvertDtoTenantToTenant(Models.Repository.Tenant tenant)
        {
            return new Tenant
            {
                TenantId = tenant.TenantId,
                TenantName = tenant.TenantName
            };
        }
    }
}
