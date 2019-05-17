using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class ItemDataService : IItemDataService
    {
        private readonly ItemRepository itemRepository;
        private readonly ItemBundleRepository itemBundleRepository;
        private readonly ItemBundleMappingRepository itemBundleMappingRepository;

        public ItemDataService(ItemRepository itemRepository,
            ItemBundleRepository itemBundleRepository,
            ItemBundleMappingRepository itemBundleMappingRepository)
        {
            this.itemRepository = itemRepository;
            this.itemBundleRepository = itemBundleRepository;
            this.itemBundleMappingRepository = itemBundleMappingRepository;
        }

        public async Task CreateItemBundleAsync(ItemBundle itemBundle)
        {
            if (itemBundle == null)
                throw new ArgumentNullException();

            await itemBundleRepository.AddItemBundleAsync(ItemMapping.CreateDtoItemBundle(itemBundle));
        }

        public async Task AddItemAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();

            await itemRepository.AddItemAsync(ItemMapping.CreateDtoItem(item));
        }

        public async Task AddItemsToBundleAsync(IEnumerable<Item> items, Guid itemBundleId)
        {
            if (items == null || itemBundleId == Guid.Empty)
                throw new ArgumentNullException();

            foreach(var item in items)
            {
                await itemBundleMappingRepository.AddItemBundleMappingAsync(item.ItemId, itemBundleId);
            }
        }

        public async Task<IEnumerable<ItemBundle>> GetItemBundlesAsync()
        {
            var itemBundles = await itemBundleRepository.GetItemBundlesAsync();

            return ItemMapping.ConvertDtoItemBundlesToItemBundles(itemBundles);
        }

        public async Task<IEnumerable<ItemBundle>> GetItemBundlesAsync(Guid tenantId)
        {
            var itemBundles = await itemBundleRepository.GetItemBundlesAsync();

            return ItemMapping.ConvertDtoItemBundlesToItemBundles(itemBundles);
        }

        public async Task<IEnumerable<Item>> GetItemsInBundleAsync()
        {
            var itemIds = await itemBundleMappingRepository.GetAllItemIdsInBundle();
            var dtoItems = await itemRepository.GetItemsByItemIdsAsync(itemIds);

            return ItemMapping.ConvertDtoItemstoItems(dtoItems);
        }

        public async Task<IEnumerable<Item>> GetItemsInBundleAsync(Guid itemBundleId)
        {
            if (itemBundleId == Guid.Empty)
                throw new ArgumentNullException();

            var itemIds = await itemBundleMappingRepository.GetAllItemIdsInBundle(itemBundleId);
            var dtoItems = await itemRepository.GetItemsByItemIdsAsync(itemIds);

            return ItemMapping.ConvertDtoItemstoItems(dtoItems);
        }

        public async Task<Item> GetItemAsync(Guid itemId)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoItem = await itemRepository.GetItemByItemIdAsync(itemId);

            return ItemMapping.ConvertDtoItemToItem(dtoItem);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(IEnumerable<Guid> itemIds)
        {
            if (itemIds == null)
                throw new ArgumentNullException();

            var dtoItems =  await itemRepository.GetItemsByItemIdsAsync(itemIds);

            return ItemMapping.ConvertDtoItemstoItems(dtoItems);
        }

        public async Task DeleteItemAsync(Guid itemId)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentNullException();

            var tool = await itemRepository.GetItemByItemIdAsync(itemId);

            await itemRepository.DeleteItemAsync(tool);
        }
    }
}
