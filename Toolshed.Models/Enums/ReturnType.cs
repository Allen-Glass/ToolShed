using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.Enums
{
    /// <summary>
    /// the state of a returned item
    /// </summary>
    public enum ReturnType
    {
        /// <summary>
        /// returns that come back at the proper time
        /// </summary>
        OnTime,

        /// <summary>
        /// User returns rental after deliver time
        /// </summary>
        Overdue,

        /// <summary>
        /// item is reported as missing
        /// </summary>
        IsMissing,

        /// <summary>
        /// user wants refund
        /// </summary>
        NeedsRefund,

        /// <summary>
        /// user does not return item prior to return
        /// </summary>
        ChargeFullPrice
    }
}
