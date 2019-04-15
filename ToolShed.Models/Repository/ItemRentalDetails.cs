﻿using System;

namespace ToolShed.Models.Repository
{
    public class ItemRentalDetails
    {
        public Guid ItemRentalDetailsId { get; set; }

        public Guid ItemId { get; set; }

        public double PricePerHour { get; set; }

        public double BaseRentalFee { get; set; }

        /// <summary>
        /// the locker number
        /// </summary>
        public string LockerNumber { get; set; }
    }
}