using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.API
{
    public class ItemBundle
    {
        public Guid ItemBundleId { get; set; }

        public Guid TenantId { get; set; }

        public string DisplayName { get; set; }
    }
}
