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
        private readonly AddressRepository addressRepository;
        private readonly DispenserItemsRepository dispenserItemRepository;

        public DispenserSQLService(DispenserRepository dispenserRepository, 
            ItemRepository itemRepository, 
            AddressRepository addressRepository)
        {
            this.dispenserRepository = dispenserRepository;
            this.itemRepository = itemRepository;
            this.addressRepository = addressRepository;
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

            var dtoDispenser = await dispenserRepository.GetDispenserByDispenserIdAsync(dispenserId);
            var itemIds = await dispenserItemRepository.GetAllItemsFromDispenserAsync(dispenserId);
            var dtoItems = await itemRepository.GetItemsByItemIdsAsync(itemIds);
            var items = ItemMapping.ConvertDtoItemstoItems(dtoItems);
            var dispenser = DispenserMapping.ConvertDtoDispenserToDispenser(dtoDispenser, items);
            dispenser.AvailableItems = items;

            return dispenser;
        }

        public async Task<string> GetDispenserStateAsync(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var addressId = await dispenserRepository.GetDispenserAddressIdAsync(dispenserId);
            return await addressRepository.GetStateAsync(addressId);
        }

        public async Task<IEnumerable<Dispenser>> GetAllDispensersAsync()
        {
            var dtoDispensers = await dispenserRepository.GetAllDispensersAsync();
            var dispensers = DispenserMapping.ConvertDtoDispensersToDispensers(dtoDispensers);            
            return dispensers;
        }

        public async Task<IEnumerable<Item>> GetAllItemsFromDispenserAsync(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var itemIds = await dispenserItemRepository.GetAllItemsFromDispenserAsync(dispenserId);
            var dtoItems = await itemRepository.GetItemsByItemIdsAsync(itemIds);

            return ItemMapping.ConvertDtoItemstoItems(dtoItems);
        }

        public async Task AddItemToDispenserAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();

            var itemId = await itemRepository.AddItemAsync(ItemMapping.CreateDtoItem(item));
        }

        public async Task AddItemsToDispenserAsync(IEnumerable<Item> items, Guid dispenserId)
        {
            if (items == null || dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            foreach (var item in items)
            {
                await AddItemToDispenserAsync(item);
            }
        }
    }
}
