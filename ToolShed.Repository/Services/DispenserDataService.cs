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
    public class DispenserDataService : IDispenserDataService
    {
        private readonly DispenserRepository dispenserRepository;
        private readonly ItemRepository itemRepository;
        private readonly AddressRepository addressRepository;
        private readonly DispenserItemsRepository dispenserItemRepository;

        public DispenserDataService(DispenserRepository dispenserRepository, 
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

            await dispenserRepository.AddAsync(DispenserMapping.ConvertDispenserToDtoDispenser(dispenser));
        }

        public async Task<Dispenser> GetDispenserAsync(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoDispenser = await dispenserRepository.GetDispenserByDispenserIdAsync(dispenserId);
            var itemIds = await dispenserItemRepository.GetAllItemsFromDispenserAsync(dispenserId);
            var dtoItems = await itemRepository.ListAsync(itemIds);
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
            var dtoDispensers = await dispenserRepository.ListAsync();
            var dispensers = DispenserMapping.ConvertDtoDispensersToDispensers(dtoDispensers);            
            return dispensers;
        }

        public async Task<IEnumerable<Item>> GetAllItemsFromDispenserAsync(Guid dispenserId)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var itemIds = await dispenserItemRepository.GetAllItemsFromDispenserAsync(dispenserId);
            var dtoItems = await itemRepository.ListAsync(itemIds);

            return ItemMapping.ConvertDtoItemstoItems(dtoItems);
        }

        public async Task AddItemToDispenserAsync(Item item)
        {
            if (item == null)
                throw new ArgumentNullException();

            var itemId = await itemRepository.AddAsync(ItemMapping.CreateDtoItem(item));
        }

        public async Task AddItemsToDispenserAsync(IEnumerable<Item> items, Guid dispenserId)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException(nameof(dispenserId));

            foreach (var item in items)
            {
                await AddItemToDispenserAsync(item);
            }
        }
    }
}
