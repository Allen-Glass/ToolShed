﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.Enums;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;
using Xunit;

namespace ToolShed.Repository.Tests.Repository
{
    public class ItemRepositoryTests
    {
        private readonly ItemRepository itemRepository;
        private readonly Item item;

        public  ItemRepositoryTests()
        {
            item = CreateNewItem();
            itemRepository = GetInMemoryItemRepository();
        }

        [Fact]
        public async Task AddItem()
        {
            await itemRepository.AddItemAsync(item);
        }

        [Fact]
        public async Task GetItemByItemId()
        {
            var itemId = await itemRepository.AddItemAsync(item);
            var dtoItem = await itemRepository.GetItemByItemIdAsync(itemId);

            Assert.Equal(item, dtoItem);
        }

        [Fact]
        public async Task GetItemsByItemIds()
        {
            var itemIds = new List<Guid>();
            for (int i = 0; i < 5; i++)
            {
                itemIds.Add(await itemRepository.AddItemAsync(item));
            }

            var items = await itemRepository.GetItemsByItemIdsAsync(itemIds);

            Assert.NotNull(items);
        }

        private Item CreateNewItem()
        {
            return new Item
            {
                ItemState = ItemState.InDispenser,
                IsAvailable = true,
                IsDamaged = false,
                IsRentable = true,
                BuyPrice = 25.00,
                DisplayName = "Wrench Knife",
                ItemTypeId = new Guid(),
                DispenserId = new Guid(),
                TenantId = new Guid()
            };
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
    }
}
