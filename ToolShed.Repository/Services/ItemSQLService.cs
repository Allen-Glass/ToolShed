using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Tools;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class ItemSQLService : IItemSQLService
    {
        private readonly ItemRepository itemRepository;

        public ItemSQLService(ItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public async Task StoreItemAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();

            await itemRepository.AddItemAsync(ItemMapping.CreateDtoItem(item));
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
