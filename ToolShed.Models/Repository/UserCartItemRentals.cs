using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.Repository
{
    public class UserCartItemRentals
    {
        public Guid UserCartItemRentalsId { get; set; }

        public Guid UserCartId { get; set; }

        public Guid ItemRentalDetailsId { get; set; }
    }
}
