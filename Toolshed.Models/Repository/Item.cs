using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Repository
{
    public class Item
    {
        /// <summary>
        /// pk of item
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ItemId { get; set; }

        /// <summary>
        /// the owning tenant
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// the dispenser at which the item is located
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// the item type
        /// </summary>
        public Guid ItemTypeId { get; set; }

        /// <summary>
        /// name of the item
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// is tool available
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// can this tool be rented
        /// </summary>
        public bool IsRentable { get; set; }

        /// <summary>
        /// the tool is damaged
        /// </summary>
        public bool IsDamaged { get; set; }

        /// <summary>
        /// the total sale price of the tool
        /// </summary>
        public double SalePrice { get; set; }

        /// <summary>
        /// the total price to buy
        /// </summary>
        public double BuyPrice { get; set; }

        /// <summary>
        /// the state of an item
        /// </summary>
        public ItemState ItemState { get; set; }
    }
}
