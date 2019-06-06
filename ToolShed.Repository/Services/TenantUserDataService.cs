using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class TenantUserDataService
    {
        private readonly TenantUserRepository tenantUserRepository;

        public TenantUserDataService(TenantUserRepository tenantUserRepository)
        {
            this.tenantUserRepository = tenantUserRepository;
        }


    }
}
