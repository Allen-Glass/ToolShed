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
    public class ItemBundleRepositoryTests
    {
        private readonly ItemBundleRepository itemBundleRepository;
        private readonly ItemBundle itemBundle;

        public ItemBundleRepositoryTests()
        {
            itemBundle = CreateNewItemBundle();
            itemBundleRepository = GetInMemoryItemBundleRepository();
        }

        [Fact]
        public async Task AddItemBundles()
        {
            await itemBundleRepository.AddItemBundleAsync(itemBundle);
        }

        [Fact]
        public async Task GetItemBundleByItemBundleId()
        {
            var itemBundleId = await itemBundleRepository.AddItemBundleAsync(itemBundle);
            var dtoItem = await itemBundleRepository.GetItemBundleAsync(itemBundleId);

            Assert.Equal(itemBundle, dtoItem);
        }

        [Fact]
        public async Task GetAllItemBundles()
        {
            var itemBundleId = await itemBundleRepository.AddItemBundleAsync(itemBundle);
            var dtoItem = await itemBundleRepository.GetItemBundlesAsync();

            Assert.Equal(itemBundle, dtoItem.FirstOrDefault());
        }

        [Fact]
        public async Task GetAllItemBundlesByTenant()
        {
            var itemBundleId = await itemBundleRepository.AddItemBundleAsync(itemBundle);
            var dtoItem = await itemBundleRepository.GetItemBundlesAsync(itemBundle.TenantId);

            Assert.Equal(itemBundle, dtoItem.FirstOrDefault());
        }

        [Fact]
        public async Task GetAllItemBundlesByBundleIds()
        {
            var itemBundleId = await itemBundleRepository.AddItemBundleAsync(itemBundle);
            var itemList = new List<Guid> { itemBundleId };
            var dtoItem = await itemBundleRepository.GetItemBundlesAsync(itemList);

            Assert.Equal(itemBundle, dtoItem.FirstOrDefault());
        }

        private ItemBundle CreateNewItemBundle()
        {
            return new ItemBundle
            {
                DisplayName = "Mac's Special",
                TenantId = Guid.NewGuid()
            };
        }

        private ItemBundleRepository GetInMemoryItemBundleRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new ItemBundleRepository(toolshedContext);
        }
    }
}
