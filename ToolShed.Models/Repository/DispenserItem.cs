using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    public class DispenserItem
    {
        /// <summary>
        /// The unique id of the tool set
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DispenserItemId { get; set; }

        /// <summary>
        /// The dispenser id
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// item pk
        /// </summary>
        public Guid ItemId { get; set; }
    }
}
