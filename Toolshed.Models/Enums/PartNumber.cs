using System;
using System.Collections.Generic;
using System.Text;

namespace Toolshed.Models.Enums
{
    /// <summary>
    /// List of all items capable of being dispensed
    /// 1000's = Single Tools
    /// 2000's = Tool sets
    /// 3000's = Dispensible items (screws, nails, etc...)
    /// 3000-3200 = screws (rounded tops)
    /// 3200-3400 = screws (flat tops)
    /// 3400-3500 = washers
    /// </summary>
    public enum PartNumber
    {
        PhillipsScrewdriver = 1000,
        FlatHeadScrewdriver = 1001,
        Hammer = 1002,
        PowerDrillKit = 2000,
        AllenWrenchSet = 2001,
        WrenchSet = 2002,
        RoundedScrew = 3000,
        FlatheadScrew = 3001
    }
}
