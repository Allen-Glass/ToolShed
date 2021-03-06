﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Repository
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string OrderName { get; set; }

        public bool IsValid { get; set; }
    }
}
