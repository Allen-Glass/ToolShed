using System;

namespace ToolShed.Models.API
{
    public class ItemRentalDetails
    {
        /// <summary>
        /// pk of item rental
        /// </summary>
        public Guid ItemRentalDetailsId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double PricePerHour { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double BaseRentalFee { get; set; }

        /// <summary>
        /// the locker number
        /// </summary>
        public string LockerNumber { get; set; }
    }
}