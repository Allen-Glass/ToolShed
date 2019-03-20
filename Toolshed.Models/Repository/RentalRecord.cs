using System;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Repository
{
    public class RentalRecord
    {
        public Guid RentalRecordId { get; set; }

        public Guid UserId { get; set; }

        public Guid RentalId { get; set; }

        public RentalAction ActionType { get; set; }

        public string Action { get; set; }
    }
}
