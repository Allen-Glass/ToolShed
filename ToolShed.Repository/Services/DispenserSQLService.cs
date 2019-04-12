using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;
using Dispenser = ToolShed.Models.API.Dispenser;

namespace ToolShed.Repository.Services
{
    public class DispenserSQLService : IDispenserSQLService
    {
        private readonly DispenserRepository dispenserRepository;
        private readonly ItemRepository itemRepository;
        private readonly DispenserToolsRepository dispenserToolsRepository;

        public DispenserSQLService(DispenserRepository dispenserRepository
            , ItemRepository itemRepository
            , DispenserToolsRepository dispenserToolsRepository)
        {
            this.dispenserRepository = dispenserRepository;
            this.itemRepository = itemRepository;
            this.dispenserToolsRepository = dispenserToolsRepository;
        }

        public async Task RegisterNewDispenserAsync(Dispenser dispenser)
        {
            if (dispenser == null)
                throw new ArgumentNullException();

            await dispenserRepository.AddDispenserAsync(DispenserMapping.ConvertDispenserToDtoDispenser(dispenser));
        }

        public async Task<Dispenser> GetDispenserAsync(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoDispenser = await dispenserRepository.GetDispenserByDispenserId(dispenserId);
            var dispenserToolIds = await dispenserToolsRepository.GetAllToolsFromDispensery(dtoDispenser.DispenserId);
            var dtoItems = await itemRepository.GetItemsByItemIdsAsync(dispenserToolIds);
            var items = ItemMapping.ConvertDtoItemstoItems(dtoItems);
            return DispenserMapping.ConvertDtoDispenserToDispenser(dtoDispenser, items);
        }

        public async Task<IEnumerable<Dispenser>> GetAllDispensersAsync()
        {
            var dtoDispensers = await dispenserRepository.GetAllDispensers();
            var dispensers = DispenserMapping.ConvertDtoDispensersToDispensers(dtoDispensers);            
            return dispensers;
        }

        public async Task<IEnumerable<Item>> GetAllItemsFromDispenserAsync(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var itemIds = await dispenserToolsRepository.GetAllToolsFromDispensery(dispenserId);
            var dtoItems = await itemRepository.GetItemsByItemIdsAsync(itemIds);

            return ItemMapping.ConvertDtoItemstoItems(dtoItems);
        }

        public async Task AddItemToDispenserAsync(Item item, Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var itemId = await itemRepository.AddItemAsync(ItemMapping.CreateDtoItem(item));
            await dispenserToolsRepository.AddToolToDispensery(DispenserMapping.CreateDispenserToolObject(dispenserId, itemId));
        }

        public async Task AddItemsToDispenserAsync(IEnumerable<Item> items, Guid dispenserId)
        {
            if (items == null || dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            foreach (var item in items)
            {
                await AddItemToDispenserAsync(item, dispenserId);
            }
        }
    }
}
