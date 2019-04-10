using System;

namespace ToolShed.Models.Repository
{
    public class ItemBundleMapping
    {
        public Guid ItemBundleMappingId { get; set; }

        public Guid ItemType { get; set; }

        public Guid ItemBundleId { get; set; }
    }
}