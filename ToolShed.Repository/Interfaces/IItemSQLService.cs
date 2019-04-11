using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Tools;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface IItemSQLService
    {
        /// <summary>
        /// Saving a tool to sql
        /// </summary>
        /// <param name="item">user submitted item object</param>
        Task AddItemAsync(Item item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<Models.Repository.Item> GetItemAsync(Guid itemId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<IEnumerable<Models.Repository.Item>> GetItemsAsync(IEnumerable<Guid> itemIds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task DeleteItemAsync(Guid itemId);
    }
}
