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
    public class UserCartItemRentalsRepositoryTests
    {
        private readonly UserCartItemRentalsRepository userCartItemRentalsRepository;
        private readonly UserCartItemRentals userCartItemRentals;
        private Guid ItemId = Guid.NewGuid();
        private Guid UserCartId = Guid.NewGuid();

        public UserCartItemRentalsRepositoryTests()
        {
            userCartItemRentals = CreateUserCartItemRentals();
            userCartItemRentalsRepository = GetInMemoryUserCartRepository();
        }

        [Fact]
        public async Task AddUserCartItems_Object()
        {
            await userCartItemRentalsRepository.AddAsync(userCartItemRentals);
        }

        [Fact]
        public async Task AddUserCartItems_List()
        {
            var list = new List<UserCartItemRentals>
            {
                userCartItemRentals,
                userCartItemRentals,
                userCartItemRentals
            };

            await userCartItemRentalsRepository.AddAsync(list);
        }

        [Fact]
        public async Task AddUserCartItems_DualParameter_Singular()
        {
            await userCartItemRentalsRepository.AddAsync(UserCartId, ItemId);
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

            await userCartItemRentalsRepository.AddAsync(UserCartId, itemList);
        }

        [Fact]
        public async Task GetItemCountInCartAsync()
        {
            var itemList = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            await userCartItemRentalsRepository.AddAsync(UserCartId, itemList);
            var itemCount = userCartItemRentalsRepository.GetItemCountInCartAsync(UserCartId);
            Assert.Equal(itemList.Count, itemCount);
        }

        [Fact]
        public async Task GetItemRentalIdsAsync_Object()
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

            await userCartItemRentalsRepository.AddAsync(UserCartId, itemList);
            //var userCartItems = await userCartItemRentalsRepository.GetUserCartItems(UserCartId);
            var hasItem1 = itemList.Contains(itemId1);
            var hasItem2 = itemList.Contains(itemId2);
            var hasItem3 = itemList.Contains(itemId3);
            Assert.True(hasItem1);
            Assert.True(hasItem2);
            Assert.True(hasItem3);
        }

        [Fact]
        public async Task GetItemRentalIdsAsync_Guid()
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

            await userCartItemRentalsRepository.AddAsync(UserCartId, itemList);
            //var userCartItems = await userCartItemRentalsRepository.GetUserCartItems(UserCartId);
            var hasItem1 = itemList.Contains(itemId1);
            var hasItem2 = itemList.Contains(itemId2);
            var hasItem3 = itemList.Contains(itemId3);
            Assert.True(hasItem1);
            Assert.True(hasItem2);
            Assert.True(hasItem3);
        }

        [Fact]
        public async Task GetUserCartItemRentals()
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

            await userCartItemRentalsRepository.AddAsync(UserCartId, itemList);
            var userCartItems = await userCartItemRentalsRepository.GetUserCartItemRentals(UserCartId);
            var hasItem1 = itemList.Contains(itemId1);
            var hasItem2 = itemList.Contains(itemId2);
            var hasItem3 = itemList.Contains(itemId3);
            Assert.True(hasItem1);
            Assert.True(hasItem2);
            Assert.True(hasItem3);
        }

        private UserCartItemRentals CreateUserCartItemRentals()
        {
            return new UserCartItemRentals
            {
                UserCartId = UserCartId,
                ItemRentalDetailsId = ItemId
            };
        }

        private UserCartItemRentalsRepository GetInMemoryUserCartRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new UserCartItemRentalsRepository(toolshedContext);
        }
    }
}
