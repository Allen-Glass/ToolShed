using System;
using System.Collections.Generic;
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

        public static IEnumerable<Tenant> ConvertDtoTenantsToTenants(IEnumerable<Models.Repository.Tenant> tenants)
        {
            var tenantList = new List<Tenant>();
            foreach (var tenant in tenants)
            {
                tenantList.Add(ConvertDtoTenantToTenant(tenant));
            }

            return tenantList;
        }
    }
}
