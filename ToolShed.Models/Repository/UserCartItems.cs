using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    public class UserCartItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserCartItemsId { get; set; }

        public Guid UserCartId { get; set; }

        public Guid ItemRentalDetailsId { get; set; }

        public Guid ItemId { get; set; }

        public Guid UserId { get; set; }
    }
}
