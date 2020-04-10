using System;

namespace ToolShed.Models.Exceptions
{
    /// <summary>
    /// Identifier returned a null entity from sql
    /// </summary>
    public class SqlEntityNullReferenceException : Exception
    {
        /// <summary>
        /// Identifier returned a null entity from sql
        /// </summary>
        /// <param name="entityType">sql entity type name</param>
        /// <param name="key">unique identifier used to acquire entity</param>
        public SqlEntityNullReferenceException(string entityType, string key)
            : base($"Sql entity type, {entityType}, with identifier, {key} could not be found.")
        {
        }

        /// <summary>
        /// Identifier returned a null entity from sql
        /// </summary>
        /// <param name="entityType">sql entity type name</param>
        /// <param name="key">unique identifier used to acquire entity</param>
        /// <param name="inner">inner exception</param>
        public SqlEntityNullReferenceException(string entityType, string key, Exception inner)
            : base($"Sql entity type, {entityType}, with identifier, {key} could not be found.", inner)
        {
        }
    }
}
