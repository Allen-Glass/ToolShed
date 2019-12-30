using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Repository
{
    public class OrderRecord
    {
        public OrderRecord()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderRecordId { get; set; }

        public Guid UserId { get; set; }

        public string OrderDescription { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public OrderDetailType OrderDetailType { get; set; }
    }
}
