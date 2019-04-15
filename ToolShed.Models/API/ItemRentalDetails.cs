using System;

namespace ToolShed.Models.API
{
    public class ItemRentalDetails
    {
        public Guid ItemRentalDetailsId { get; set; }

        public Item Item { get; set; }

        public double PricePerHour { get; set; }

        public double BaseRentalFee { get; set; }

        /// <summary>
        /// the locker number
        /// </summary>
        public string LockerNumber { get; set; }
    }
}