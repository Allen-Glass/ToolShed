using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.Enums
{
    public enum ItemState
    {
        InDispenser,

        Reserved,

        CurrentlyRented,

        Missing,

        Damaged,

        UserOwned
    }
}
