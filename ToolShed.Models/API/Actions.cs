using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.Enums;

namespace ToolShed.Models.API
{
    public class Actions
    {
        public ActionType ActionType { get; set; }

        public string LockerNumber { get; set; }

        public string LockerCode { get; set; }
    }
}
