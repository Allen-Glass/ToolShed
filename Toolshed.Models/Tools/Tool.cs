﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolshed.Models.Enums;

namespace Toolshed.Models.Tools
{
    /// <summary>
    /// The tool to be or has been rented objected
    /// </summary>
    public class Tool
    {
        /// <summary>
        /// pk of tool
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ToolId { get; set; }

        /// <summary>
        /// Display name of tool
        /// </summary>
        public string ToolName { get; set; }

        /// <summary>
        /// unique identifier of tool type
        /// </summary>
        public PartNumber ToolType { get; set; }

        /// <summary>
        /// The parent dispenser
        /// </summary>
        public Guid DispenserId { get; set; }

        /// <summary>
        /// Tool availability
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Tool health
        /// </summary>
        public bool NeedsRepair { get; set; }

        /// <summary>
        /// tool needs inspection
        /// </summary>
        public bool NeedsInspection { get; set; }

        /// <summary>
        /// tool needs replacement
        /// </summary>
        public bool NeedsReplacement { get; set; }

        /// <summary>
        /// Tool not reported on
        /// </summary>
        public bool IsMissing { get; set; }

        /// <summary>
        /// cost of the actual tool
        /// </summary>
        public double ToolCost { get; set; }

        /// <summary>
        /// date when tool is first accounted for in logistics cycle
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// the date when the tool is assigned to a dispenser
        /// </summary>
        public DateTime AssignmentDate { get; set; }

        /// <summary>
        /// Date of last inspection
        /// </summary>
        public DateTime LastInspection { get; set; }

        /// <summary>
        /// date when tool is to be decommissioned
        /// </summary>
        public DateTime DecommissionDate { get; set; }
    }
}
