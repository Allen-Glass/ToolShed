using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Dispensers;
using Toolshed.Models.Tools;

namespace ToolShed.Repository.Interfaces
{
    public interface IDispenserSQLService
    {
        /// <summary>
        /// register a new dispenser
        /// </summary>
        /// <param name="dispenser"></param>
        /// <returns></returns>
        Task RegisterNewDispenserAsync(Dispenser dispenser);

        /// <summary>
        /// get a dispenser
        /// </summary>
        /// <param name="dispenserId">pk of dispenser</param>
        /// <returns>dispenser</returns>
        Task<Dispenser> GetDispenser(Guid dispenserId);

        /// <summary>
        /// Get all dispensers
        /// </summary>
        /// <returns>list of all dispensers</returns>
        Task<IEnumerable<Dispenser>> GetAllDispensers();

        /// <summary>
        /// Get all the tools associated with a dispenser
        /// </summary>
        /// <param name="dispenserId">pk of dispenser</param>
        /// <returns></returns>
        Task<IEnumerable<Tool>> GetAllToolsFromDispenser(Guid dispenserId);

        /// <summary>
        /// Add a tool to a dispenser
        /// </summary>
        /// <param name="tool">tool object</param>
        /// <param name="dispenserId">pk of dispenser</param>
        /// <returns></returns>
        Task AddToolToDispenser(Tool tool, Guid dispenserId);

        /// <summary>
        /// add a list of tools to a dispenser
        /// </summary>
        /// <param name="tools">list of tools</param>
        /// <param name="dispenserId">pk of dispenser</param>
        /// <returns></returns>
        Task AddToolsToDispenser(IEnumerable<Tool> tools, Guid dispenserId);
    }
}
