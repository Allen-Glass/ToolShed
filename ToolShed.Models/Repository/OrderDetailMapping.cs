using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToolShed.Models.Repository
{
    public class OrderDetailMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }

        public Guid ItemId { get; set; }
    }
}
