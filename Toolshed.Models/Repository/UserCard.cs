using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolShed.Models.Repository
{
    /// <summary>
    /// table to associate users to credit cards
    /// </summary>
    public class UserCard
    {
        /// <summary>
        /// pk of usercard table
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserCardId { get; set; }

        /// <summary>
        /// pk of user
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// pk of credit cards
        /// </summary>
        public Guid CardId { get; set; }
    }
}
