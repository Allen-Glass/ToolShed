using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    public class ItemRentalDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ItemRentalDetailsId { get; set; }

        public Guid ItemId { get; set; }

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