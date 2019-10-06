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
    public class UserCartRepositoryTests
    {
        private readonly UserCartRepository userCartRepository;
        private readonly UserCart userCart;
        private Guid UserId = Guid.NewGuid();
        private Guid UserCartId = Guid.NewGuid();

        public UserCartRepositoryTests()
        {
            userCartRepository = GetInMemoryUserCartRepository();
            userCart = CreateUserCart();
        }

        [Fact]
        public async Task AddUserCart()
        {
            await userCartRepository.AddAsync(userCart);
        }

        [Fact]
        public async Task DoesUserHaveItemsInCart_Success()
        {
            await userCartRepository.AddAsync(userCart);
            var hasItem = await userCartRepository.DoesUserHaveItemsInCart(UserId);

            Assert.True(hasItem);
        }

        [Fact]
        public async Task DoesUserHaveItemsInCart_Failure()
        {
            await userCartRepository.AddAsync(userCart);
            var hasItem = await userCartRepository.DoesUserHaveItemsInCart(Guid.NewGuid()); ;

            Assert.False(hasItem);
        }

        [Fact]
        public async Task GetActiveUserCart()
        {
            await userCartRepository.AddAsync(this.userCart);
            var userCart = await userCartRepository.GetAsync(UserId);

            Assert.Equal(this.userCart, userCart);
        }

        [Fact]
        public async Task GetUserCartId()
        {
            await userCartRepository.AddAsync(this.userCart);
            var userCartId = await userCartRepository.GetUserCartIdAsync(UserId);

            Assert.Equal(UserCartId, userCartId);
        }

        private UserCart CreateUserCart()
        {
            return new UserCart
            {
                UserId = UserId,
                UserCartId = UserCartId
            };
        }

        private UserCartRepository GetInMemoryUserCartRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new UserCartRepository(toolshedContext);
        }
    }
}
