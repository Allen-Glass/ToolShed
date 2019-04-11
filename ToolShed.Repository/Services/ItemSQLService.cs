using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class ItemSQLService : IItemSQLService
    {
        private readonly ItemRepository itemRepository;
        private readonly ItemBundleRepository itemBundleRepository;
        private readonly ItemBundleMappingRepository itemBundleMappingRepository;

        public ItemSQLService(ItemRepository itemRepository,
            ItemBundleRepository itemBundleRepository,
            ItemBundleMappingRepository itemBundleMappingRepository)
        {
            this.itemRepository = itemRepository;
            this.itemBundleRepository = itemBundleRepository;
            this.itemBundleMappingRepository = itemBundleMappingRepository;
        }

        public async Task AddItemAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();

            await itemRepository.AddItemAsync(ItemMapping.CreateDtoItem(item));
        }

        public async Task<IEnumerable<Models.Repository.Item>> GetItemsInBundleAsync(Guid itemBundleId)
        {
            if (itemBundleId == Guid.Empty)
                throw new ArgumentNullException();

            var itemIds = await itemBundleMappingRepository.GetAllItemIdsInBundle(itemBundleId);
            return await itemRepository.GetItemsByItemIdsAsync(itemIds);
        }

        public async Task<Models.Repository.Item> GetItemAsync(Guid itemId)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentNullException();

            return await itemRepository.GetItemByItemIdAsync(itemId);
        }

        public async Task<IEnumerable<Models.Repository.Item>> GetItemsAsync(IEnumerable<Guid> itemIds)
        {
            if (itemIds == null)
                throw new ArgumentNullException();

            return await itemRepository.GetItemsByItemIdsAsync(itemIds);
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
