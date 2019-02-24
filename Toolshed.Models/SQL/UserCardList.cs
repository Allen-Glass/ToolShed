﻿using System;

namespace Toolshed.Models.SQL
{
    /// <summary>
    /// table to associate users to credit cards
    /// </summary>
    public class UserCardList
    {
        /// <summary>
        /// pk of usercard table
        /// </summary>
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
