using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toolshed.Models.SQL
{
    public class DispenserTool
    {
        /// <summary>
        /// The unique id of the tool set
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DispenserToolId { get; set; }

        /// <summary>
        /// The dispenser id
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// Tool pk
        /// </summary>
        public Guid ToolId { get; set; }
    }
}
