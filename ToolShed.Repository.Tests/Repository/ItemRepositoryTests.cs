using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.Enums;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Tests.Repository
{
    public class ItemRepositoryTests
    {
        private readonly ItemRepository itemRepository;
        private readonly Item item;

        public  ItemRepositoryTests()
        {

        }

        private Item CreateNewCard()
        {
            return new Item
            {
                ItemState = ItemState.InDispenser,
                i = "John",
                CardNumber = "0987-9789-9873-0083",
                UserId = new Guid("12345678-1234-1234-1234-123456789012")
            };
        }

        private ItemRepository GetInMemoryCardRepository()
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
