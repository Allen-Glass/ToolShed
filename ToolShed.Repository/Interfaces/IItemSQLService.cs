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
        /// add list of items to a bundle
        /// </summary>
        /// <param name="items"></param>
        /// <param name="itemBundleId"></param>
        /// <returns></returns>
        Task AddItemsToBundleAsync(IEnumerable<Item> items, Guid itemBundleId);

        /// <summary>
        /// get itembundles
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ItemBundle>> GetItemBundlesAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<ItemBundle>> GetItemBundlesAsync(Guid tenantId);

        /// <summary>
        /// get items in a bundle
        /// </summary>
        /// <returns>list of items</returns>
        Task<IEnumerable<Item>> GetItemsInBundleAsync();

        /// <summary>
        /// grab all items associated in a bundle
        /// </summary>
        /// <param name="itemBundleId">item bundle id</param>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetItemsInBundleAsync(Guid itemBundleId);

        /// <summary>
        /// get an item from item id
        /// </summary>
        /// <param name="itemId">item id</param>
        /// <returns></returns>
        Task<Item> GetItemAsync(Guid itemId);

        /// <summary>
        /// get a list of items from their ids
        /// </summary>
        /// <param name="itemId">item id</param>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetItemsAsync(IEnumerable<Guid> itemIds);

        /// <summary>
        /// delete an item with its id
        /// </summary>
        /// <param name="itemId">item id</param>
        /// <returns></returns>
        Task DeleteItemAsync(Guid itemId);
    }
}
