using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Repository
{
    public class RentalRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RentalRecordId { get; set; }

        public Guid UserId { get; set; }

        public Guid RentalId { get; set; }

        public RentalAction ActionType { get; set; }

        public string Action { get; set; }
    }
}
