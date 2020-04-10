using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface IItemDataService
    {
        /// <summary>
        /// Saving a tool to sql
        /// </summary>
        /// <param name="item">user submitted item object</param>
        Task AddItemAsync(Item item, CancellationToken cancellationToken = default);

        /// <summary>
        /// add list of items to a bundle
        /// </summary>
        /// <param name="items"></param>
        /// <param name="itemBundleId"></param>
        /// <returns></returns>
        Task AddItemsToBundleAsync(IEnumerable<Item> items, Guid itemBundleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// get itembundles
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ItemBundle>> GetItemBundlesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<ItemBundle>> GetItemBundlesAsync(Guid tenantId, CancellationToken cancellationToken = default);

        /// <summary>
        /// get items in a bundle
        /// </summary>
        /// <returns>list of items</returns>
        Task<IEnumerable<Item>> GetItemsInBundleAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// grab all items associated in a bundle
        /// </summary>
        /// <param name="itemBundleId">item bundle id</param>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetItemsInBundleAsync(Guid itemBundleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// get an item from item id
        /// </summary>
        /// <param name="itemId">item id</param>
        /// <returns></returns>
        Task<Item> GetItemAsync(Guid itemId, CancellationToken cancellationToken = default);

        /// <summary>
        /// get a list of items from their ids
        /// </summary>
        /// <param name="itemId">item id</param>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetItemsAsync(IEnumerable<Guid> itemIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// delete an item with its id
        /// </summary>
        /// <param name="itemId">item id</param>
        /// <returns></returns>
        Task DeleteItemAsync(Guid itemId, CancellationToken cancellationToken = default);
    }
}
