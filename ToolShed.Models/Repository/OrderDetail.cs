﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Repository
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderDetailsId { get; set; }

        public Guid OrderId { get; set; }
        
        public Guid ItemId { get; set; }

        public Guid ItemRentalDetailId { get; set; }

        public double Pricing { get; set; }

        public OrderDetailType OrderDetailType { get; set; }
    }
}
