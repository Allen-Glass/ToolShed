using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShed.Models.API;

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
        Task<Dispenser> GetDispenserAsync(Guid dispenserId);

        /// <summary>
        /// get the state of where the rental took place for tax purposes
        /// </summary>
        /// <param name="dispenserId">pk of dispenser</param>
        /// <returns>state of dispenser</returns>
        Task<string> GetDispenserStateAsync(Guid dispenserId);

        /// <summary>
        /// Get all dispensers
        /// </summary>
        /// <returns>list of all dispensers</returns>
        Task<IEnumerable<Dispenser>> GetAllDispensersAsync();

        /// <summary>
        /// Get all the tools associated with a dispenser
        /// </summary>
        /// <param name="dispenserId">pk of dispenser</param>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetAllItemsFromDispenserAsync(Guid dispenserId);

        /// <summary>
        /// Add a tool to a dispenser
        /// </summary>
        /// <param name="tool">tool object</param>
        /// <param name="dispenserId">pk of dispenser</param>
        /// <returns></returns>
        Task AddItemToDispenserAsync(Item item);

        /// <summary>
        /// add a list of tools to a dispenser
        /// </summary>
        /// <param name="tools">list of tools</param>
        /// <param name="dispenserId">pk of dispenser</param>
        /// <returns></returns>
        Task AddItemsToDispenserAsync(IEnumerable<Item> items, Guid dispenserId);
    }
}
