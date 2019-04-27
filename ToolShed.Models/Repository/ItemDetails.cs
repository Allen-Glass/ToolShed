using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Models.Repository
{
    public class ItemDetails
    {
        /// <summary>
        /// pk of item description
        /// </summary>
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
