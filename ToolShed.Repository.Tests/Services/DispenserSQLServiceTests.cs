using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using ToolShed.Models.API;
using ToolShed.Models.Enums;
using ToolShed.Repository.Context;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;
using ToolShed.Repository.Services;
using Xunit;

namespace ToolShed.Repository.Tests.Services
{
    public class DispenserSQLServiceTests
    {
        private readonly IDispenserDataService dispenserSQLService;
        private readonly Dispenser dispenser;

        public DispenserSQLServiceTests()
        {
            dispenserSQLService = CreateDispenserSQLService();
            dispenser = CreateDispenser();
        }

        [Fact]
        public async Task RegisterNewDispenserAsync()
        {
            await dispenserSQLService.RegisterNewDispenserAsync(dispenser);
        }

        private Dispenser CreateDispenser()
        {
            return new Dispenser
            {
                CreationDate = DateTime.UtcNow,
                LastMaintenanceCheckDate = DateTime.UtcNow,
                DecommishDate = DateTime.UtcNow,
                DispenserIotName = "MAH_PIE",
                DispenserAddress = CreateAddress(),
                DispenserName = "MAH_PIE",
                AvailableItems = CreateItemList()
            };
        }

        private IEnumerable<Item> CreateItemList()
        {
            var itemList = new List<Item>();
            var item = new Item
            {
                IsAvailable = true,
                IsDamaged = false,
                IsRentable = true,
                ItemState = ItemState.InDispenser,
                BuyPrice = 10.00,
                DisplayName = "Jack Knife",
                ItemId = Guid.NewGuid(),
                ItemType = Guid.NewGuid(),
                SalePrice = 12.50,
                TenantId = Guid.NewGuid()
            };
            itemList.Add(item);

            return itemList;
        }

        private Address CreateAddress()
        {
            return new Address
            {
                AddressType = AddressType.Dispenser,
                AptNumber = "717",
                City = "NYC",
                Country = "USA",
                State = "NY",
                StreetName = "3213 Middle Ln",
                StreetName2 = "",
                ZipCode = "98765"
            };
        }

        private IDispenserDataService CreateDispenserSQLService()
        {
            return new DispenserDataService(GetInMemoryDispenserRepository(),
                GetInMemoryItemRepository(),
                GetInMemoryAddressRepository());
        }

        private DispenserRepository GetInMemoryDispenserRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new DispenserRepository(toolshedContext);
        }

        private ItemRepository GetInMemoryItemRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new ItemRepository(toolshedContext);
        }

        private AddressRepository GetInMemoryAddressRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new AddressRepository(toolshedContext);
        }
    }
}
