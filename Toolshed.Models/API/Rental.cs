using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.API
{
    public class Rental
    {
        /// <summary>
        /// pk of rental
        /// </summary>
        public Rental RentalId { get; set; }

        public User User { get; set; }

        public DateTime RentalStart { get; set; }
        
        public DateTime RentalDue { get; set; }
        
        public DateTime RentalReturned { get; set; }

        public double PricePerHour { get; set; }
        public double PricePerHourOver { get; set; }
    }
}
