using System;

namespace ToolShed.Models.Repository
{
    /// <summary>
    /// Tenant Mapping to User
    /// </summary>
    public class TenantUser
    {
        public Guid TenantUserId { get; set; }

        public Guid TenantId { get; set; }

        public Guid UserId { get; set; }
    }
}
