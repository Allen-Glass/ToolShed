using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Enums;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;
using Xunit;

namespace ToolShed.Repository.Tests.Repository
{
    public class ItemBundleMappingRepositoryTests
    {
        private readonly ItemBundleMappingRepository itemBundleMappingRepository;
        private readonly ItemBundleMapping itemBundleMapping;

        public ItemBundleMappingRepositoryTests()
        {
            itemBundleMapping = CreateNewItemBundle();
            itemBundleMappingRepository = GetInMemoryItemBundleMappingRepository();
        }

        [Fact]
        public async Task AddItemBundles()
        {
            await itemBundleMappingRepository.AddItemBundleMappingAsync(itemBundleMapping);
        }

        [Fact]
        public async Task AddItemBundlesObjectless()
        {
            await itemBundleMappingRepository.AddItemBundleMappingAsync(new Guid(), new Guid());
        }

        [Fact]
        public async Task GetItemBundleMapping()
        {
            await itemBundleMappingRepository.AddItemBundleMappingAsync(itemBundleMapping);
            var itemBundleMappingId = await itemBundleMappingRepository.GetItemBundleIdFromItemId(itemBundleMapping.ItemId);

            Assert.Equal(itemBundleMapping.ItemBundleId, itemBundleMappingId);
        }

        [Fact]
        public async Task GetAllItemBundles()
        {
            var itemBundleId = await itemBundleMappingRepository.AddItemBundleMappingAsync(itemBundleMapping);
            var dtoItems = await itemBundleMappingRepository.GetItemBundleMappingAsync();

            Assert.Equal(itemBundleMapping, dtoItems.FirstOrDefault());
        }

        [Fact]
        public async Task GetItemBundlesByItemBundleId()
        {
            var itemBundleId = await itemBundleMappingRepository.AddItemBundleMappingAsync(itemBundleMapping);
            var dtoItem = await itemBundleMappingRepository.GetItemBundleMappingAsync(itemBundleId);

            Assert.Equal(itemBundleMapping, dtoItem);
        }

        private ItemBundleMapping CreateNewItemBundle()
        {
            return new ItemBundleMapping
            {
                ItemBundleId = new Guid(),
                ItemId = new Guid()
            };
        }

        private ItemBundleMappingRepository GetInMemoryItemBundleMappingRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new ItemBundleMappingRepository(toolshedContext);
        }
    }
}
