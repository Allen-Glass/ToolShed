using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toolshed.Models.Dispensers
{
    public class DispensibleItemsList
    {
        /// <summary>
        /// pk of dispensible items
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DispensibleItemsListId { get; set; }
    }
}
