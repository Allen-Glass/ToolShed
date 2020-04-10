using System;
using System.Collections.Generic;
using System.Threading;
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
            this.dispenserRepository = dispenserRepository ?? throw new ArgumentNullException(nameof(dispenserRepository));
            this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            this.addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task RegisterNewDispenserAsync(Dispenser dispenser, CancellationToken cancellationToken = default)
        {
            if (dispenser == null)
                throw new ArgumentNullException(nameof(dispenser));

            await dispenserRepository.AddAsync(dispenser.ConvertDispenserToDtoDispenser(), cancellationToken);
        }

        public async Task<Dispenser> GetDispenserAsync(Guid dispenserId, CancellationToken cancellationToken = default)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoDispenser = await dispenserRepository.GetDispenserByDispenserIdAsync(dispenserId, cancellationToken);
            var itemIds = await dispenserItemRepository.GetAllItemsFromDispenserAsync(dispenserId, cancellationToken);
            var dtoItems = await itemRepository.ListAsync(itemIds, cancellationToken);
            var items = dtoItems.ConvertDtoItemstoItems();
            var dispenser = dtoDispenser.ConvertDtoDispenserToDispenser(items);
            dispenser.AvailableItems = items;

            return dispenser;
        }

        public async Task<string> GetDispenserStateAsync(Guid dispenserId, CancellationToken cancellationToken = default)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException(nameof(dispenserId));

            var addressId = await dispenserRepository.GetDispenserAddressIdAsync(dispenserId, cancellationToken);
            return await addressRepository.GetStateAsync(addressId, cancellationToken);
        }

        public async Task<IEnumerable<Dispenser>> GetAllDispensersAsync(CancellationToken cancellationToken = default)
        {
            var dtoDispensers = await dispenserRepository.ListAsync(cancellationToken);
            var dispensers = dtoDispensers.ConvertDtoDispensersToDispensers();            
            return dispensers;
        }

        public async Task<IEnumerable<Item>> GetAllItemsFromDispenserAsync(Guid dispenserId, CancellationToken cancellationToken = default)
        {
            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException(nameof(dispenserId));

            var itemIds = await dispenserItemRepository.GetAllItemsFromDispenserAsync(dispenserId, cancellationToken);
            var dtoItems = await itemRepository.ListAsync(itemIds, cancellationToken);

            return dtoItems.ConvertDtoItemstoItems();
        }

        public async Task AddItemToDispenserAsync(Item item, CancellationToken cancellationToken = default)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            await itemRepository.AddAsync(ItemMapping.CreateDtoItem(item), cancellationToken);
        }

        public async Task AddItemsToDispenserAsync(IEnumerable<Item> items, Guid dispenserId, CancellationToken cancellationToken = default)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (dispenserId == Guid.Empty)
                throw new ArgumentNullException(nameof(dispenserId));

            foreach (var item in items)
            {
                await AddItemToDispenserAsync(item, cancellationToken);
            }
        }
    }
}
