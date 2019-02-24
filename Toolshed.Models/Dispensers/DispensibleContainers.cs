using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolshed.Models.Enums;

namespace Toolshed.Models.Dispensers
{
    public class DispensibleContainers
    {
        /// <summary>
        /// pk of Dispenser Containers
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DispensibleContainersId { get; set; }

        /// <summary>
        /// percentage filled
        /// </summary>
        public double PercentFilled { get; set; }

        /// <summary>
        /// The dispensable item type
        /// </summary>
        public PartNumber DispensibleType { get; set; }
    }
}
