using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    public class ItemBundleMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ItemBundleMappingId { get; set; }

        public Guid ItemId { get; set; }

        public Guid ItemBundleId { get; set; }
    }
}