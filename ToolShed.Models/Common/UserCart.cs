using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.API
{
    public class UserCart
    {
        public Guid UserCartId { get; set; }

        public Guid UserId { get; set; }

        public IEnumerable<Guid> ItemIds { get; set; }

        public IEnumerable<Guid> ItemRentalIds { get; set; }

        public IEnumerable<Item> Items { get; set; }

        public IEnumerable<ItemRentalDetails> ItemRentals { get; set; }
    }
}
