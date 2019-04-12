using System;

namespace ToolShed.Models.Repository
{
    public class ItemBundleMapping
    {
        public Guid ItemBundleMappingId { get; set; }

        public Guid ItemId { get; set; }

        public Guid ItemBundleId { get; set; }
    }
}