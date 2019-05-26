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
    public class UserCartItemsRepositoryTests
    {
        private readonly UserCartItemsRepository userCartItemsRepository;
        private readonly UserCartItems userCartItems;
        private Guid ItemId = Guid.NewGuid();
        private Guid UserCartId = Guid.NewGuid();

        public UserCartItemsRepositoryTests()
        {
            userCartItems = CreateUserCartItems();
            userCartItemsRepository = GetInMemoryUserCartRepository();
        }

        [Fact]
        public async Task AddUserCartItems_Object()
        {
            await userCartItemsRepository.AddUserCartItemsAsync(userCartItems);
        }

        [Fact]
        public async Task AddUserCartItems_List()
        {
            var list = new List<UserCartItems>
            {
                userCartItems,
                userCartItems,
                userCartItems
            };

            await userCartItemsRepository.AddUserCartItemsAsync(list);
        }

        [Fact]
        public async Task AddUserCartItems_DualParameter_Singular()
        {
            await userCartItemsRepository.AddUserCartItemsAsync(UserCartId, ItemId);
        }

        [Fact]
        public async Task AddUserCartItems_DualParameter_List()
        {
            var itemList = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            await userCartItemsRepository.AddUserCartItemsAsync(UserCartId, itemList);
        }

        [Fact]
        public async Task GetItemCountInCart()
        {
            var itemList = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            await userCartItemsRepository.AddUserCartItemsAsync(UserCartId, itemList);
            var itemCount = userCartItemsRepository.GetItemCountInCartAsync(UserCartId);
            Assert.Equal(itemList.Count, itemCount);
        }

        [Fact]
        public async Task GetUserCartItems()
        {
            var itemId1 = Guid.NewGuid();
            var itemId2 = Guid.NewGuid();
            var itemId3 = Guid.NewGuid();
            var itemList = new List<Guid>
            {
                itemId1,
                itemId2,
                itemId3
            };

            await userCartItemsRepository.AddUserCartItemsAsync(UserCartId, itemList);
            var userCartItems = await userCartItemsRepository.GetUserCartItems(UserCartId);
            var hasItem1 = itemList.Contains(itemId1);
            var hasItem2 = itemList.Contains(itemId2);
            var hasItem3 = itemList.Contains(itemId3);
            Assert.True(hasItem1);
            Assert.True(hasItem2);
            Assert.True(hasItem3);
        }

        [Fact]
        public async Task GetUserCartItemIds()
        {
            var itemId1 = Guid.NewGuid();
            var itemId2 = Guid.NewGuid();
            var itemId3 = Guid.NewGuid();
            var itemList = new List<Guid>
            {
                itemId1,
                itemId2,
                itemId3
            };

            await userCartItemsRepository.AddUserCartItemsAsync(UserCartId, itemList);
            var userCartItems = await userCartItemsRepository.GetUserCartItems(UserCartId);
            var hasItem1 = itemList.Contains(itemId1);
            var hasItem2 = itemList.Contains(itemId2);
            var hasItem3 = itemList.Contains(itemId3);
            Assert.True(hasItem1);
            Assert.True(hasItem2);
            Assert.True(hasItem3);
        }

        private UserCartItems CreateUserCartItems()
        {
            return new UserCartItems
            {
                UserCartId = UserCartId,
                ItemId = ItemId
            };
        }

        private UserCartItemsRepository GetInMemoryUserCartRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new UserCartItemsRepository(toolshedContext);
        }
    }
}
