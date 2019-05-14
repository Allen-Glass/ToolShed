using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;
using Xunit;

namespace ToolShed.Repository.Tests.Repository
{
    public class DispenserItemRepositoryTests
    {
        private readonly DispenserItemsRepository dispenserItemRepository;
        private readonly DispenserItem dispenserItem;

        public DispenserItemRepositoryTests()
        {
            dispenserItemRepository = GetInMemoryDispenserItemRepository();
            dispenserItem = CreateDispenserItem();
        }

        [Fact]
        public async Task AddItemToDispenserAsync()
        {
            await dispenserItemRepository.AddItemToDispenserAsync(dispenserItem.ItemId, dispenserItem.DispenserId);
        }

        [Fact]
        public async Task GetDispenserByItemIdAsync()
        {
            await dispenserItemRepository.AddItemToDispenserAsync(dispenserItem.ItemId, dispenserItem.DispenserId);
            var dispenserId = await dispenserItemRepository.GetDispenserByItemIdAsync(dispenserItem.ItemId);

            Assert.Equal(dispenserItem.DispenserId, dispenserId);
        }

        [Fact]
        public async Task GetAllItemsFromDispenserAsync()
        {
            await dispenserItemRepository.AddItemToDispenserAsync(dispenserItem.ItemId, dispenserItem.DispenserId);
            var itemIds = await dispenserItemRepository.GetAllItemsFromDispenserAsync(dispenserItem.DispenserId);

            Assert.Equal(dispenserItem.ItemId, itemIds.FirstOrDefault());
        }

        private DispenserItem CreateDispenserItem()
        {
            return new DispenserItem
            {
                ItemId = Guid.NewGuid(),
                DispenserId = Guid.NewGuid()
            };
        }

        private DispenserItemsRepository GetInMemoryDispenserItemRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new DispenserItemsRepository(toolshedContext);
        }
    }
}
