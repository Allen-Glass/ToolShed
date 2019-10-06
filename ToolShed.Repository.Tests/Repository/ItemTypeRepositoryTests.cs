using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;
using Xunit;

namespace ToolShed.Repository.Tests.Repository
{
    public class ItemTypeRepositoryTests
    {
        private readonly ItemTypeRepository itemTypeRepository;
        private readonly ItemType itemType;

        public ItemTypeRepositoryTests()
        {
            itemType = CreateNewItemType();
            itemTypeRepository = GetInMemoryItemTypeRepository();
        }

        [Fact]
        public async Task AddItem()
        {
            await itemTypeRepository.AddAsync(itemType);
        }

        [Fact]
        public async Task GetItemByItemId()
        {
            var itemTypeId = await itemTypeRepository.AddAsync(itemType);
            var dtoItemType = await itemTypeRepository.GetAsync(itemTypeId);

            Assert.Equal(itemType, dtoItemType);
        }

        private ItemType CreateNewItemType()
        {
            return new ItemType
            {
                DisplayName = "Knife Wrench",
                Manufacturer = "Mac's Goods"
            };
        }

        private ItemTypeRepository GetInMemoryItemTypeRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new ItemTypeRepository(toolshedContext);
        }
    }
}
