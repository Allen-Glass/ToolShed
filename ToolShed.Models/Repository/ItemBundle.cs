﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    public class ItemBundle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ItemBundleId { get; set; }

        public Guid TenantId { get; set; }

        public string DisplayName { get; set; }

        public IEnumerable<ItemBundle> FirstOrDefault()
        {
            throw new NotImplementedException();
        }
    }
}
