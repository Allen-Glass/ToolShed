using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.API;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Common.Replacement
{
    public class ReplacementDetails
    {
        public ReplacementOrderProvider ReplacementOrderProvider { get; set; }

        public ReplacementPeriod ReplacementPeriod { get; set; }

        public int Frequency { get; set; }

        public User User { get; set; }

        public bool IsActive { get; set; }
    }
}
