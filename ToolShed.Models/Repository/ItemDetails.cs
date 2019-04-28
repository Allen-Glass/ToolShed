using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToolShed.Models.Repository
{
    public class ItemDetails
    {
        /// <summary>
        /// pk of item description
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ItemDetailsId { get; set; }

        /// <summary>
        /// item description
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// year of release
        /// </summary>
        public int ModelYear { get; set; }

        /// <summary>
        /// item color
        /// </summary>
        public string Color { get; set; }
    }
}
