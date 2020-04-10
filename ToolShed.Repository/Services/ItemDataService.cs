using System;
using System.Collections.Generic;
using System.Threading;
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
            this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            this.itemBundleRepository = itemBundleRepository ?? throw new ArgumentNullException(nameof(itemBundleRepository));
            this.itemBundleMappingRepository = itemBundleMappingRepository ?? throw new ArgumentNullException(nameof(itemBundleMappingRepository));
        }

        public async Task CreateItemBundleAsync(ItemBundle itemBundle, CancellationToken cancellationToken = default)
        {
            if (itemBundle == null)
                throw new ArgumentNullException(nameof(itemBundle));

            await itemBundleRepository.AddAsync(itemBundle.CreateDtoItemBundle(), cancellationToken);
        }

        public async Task AddItemAsync(Item item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await itemRepository.AddAsync(item.CreateDtoItem(), cancellationToken);
        }

        public async Task AddItemsToBundleAsync(IEnumerable<Item> items, Guid itemBundleId, CancellationToken cancellationToken = default)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (itemBundleId == Guid.Empty)
                throw new ArgumentNullException(nameof(itemBundleId));

            foreach (var item in items)
            {
                await itemBundleMappingRepository.AddItemBundleMappingAsync(item.ItemId, itemBundleId, cancellationToken);
            }
        }

        public async Task<IEnumerable<ItemBundle>> GetItemBundlesAsync(CancellationToken cancellationToken = default)
        {
            var itemBundles = await itemBundleRepository.ListAsync(cancellationToken);

            return ItemMapping.ConvertDtoItemBundlesToItemBundles(itemBundles);
        }

        public async Task<IEnumerable<ItemBundle>> GetItemBundlesAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var itemBundles = await itemBundleRepository.ListAsync(cancellationToken);

            return itemBundles.ConvertDtoItemBundlesToItemBundles();
        }

        public async Task<IEnumerable<Item>> GetItemsInBundleAsync(CancellationToken cancellationToken = default)
        {
            var itemIds = await itemBundleMappingRepository.GetAllItemIdsInBundle(cancellationToken);
            var dtoItems = await itemRepository.ListAsync(itemIds);

            return dtoItems.ConvertDtoItemstoItems();
        }

        public async Task<IEnumerable<Item>> GetItemsInBundleAsync(Guid itemBundleId, CancellationToken cancellationToken = default)
        {
            if (itemBundleId == Guid.Empty)
                throw new ArgumentNullException(nameof(itemBundleId));

            var itemIds = await itemBundleMappingRepository.GetAllItemIdsInBundle(itemBundleId, cancellationToken);
            var dtoItems = await itemRepository.ListAsync(itemIds, cancellationToken);

            return dtoItems.ConvertDtoItemstoItems();
        }

        public async Task<Item> GetItemAsync(Guid itemId, CancellationToken cancellationToken = default)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentNullException(nameof(itemId));

            var dtoItem = await itemRepository.GetAsync(itemId, cancellationToken);

            return dtoItem.ConvertDtoItemToItem();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(IEnumerable<Guid> itemIds, CancellationToken cancellationToken = default)
        {
            if (itemIds == null)
                throw new ArgumentNullException(nameof(itemIds));

            var dtoItems =  await itemRepository.ListAsync(itemIds, cancellationToken);

            return dtoItems.ConvertDtoItemstoItems();
        }

        public async Task DeleteItemAsync(Guid itemId, CancellationToken cancellationToken = default)
        {
            if (itemId == Guid.Empty)
                throw new ArgumentNullException(nameof(itemId));

            var tool = await itemRepository.GetAsync(itemId, cancellationToken);

            await itemRepository.DeleteAsync(tool);
        }
    }
}
