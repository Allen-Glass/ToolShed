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
    public class UserAddressesRepositoryTests
    {
        private readonly UserAddressesRepository userAddressRepository;
        private readonly UserAddresses userAddress;

        public UserAddressesRepositoryTests()
        {
            userAddressRepository = GetInMemoryUserAddressesRepository();
            userAddress = CreateUserAddresses();
        }

        [Fact]
        public async Task AddUserAddresses()
        {
            await userAddressRepository.AddUserAddressAsync(userAddress);
        }

        [Fact]
        public async Task GetAddressesByUserId()
        {
            await userAddressRepository.AddUserAddressAsync(userAddress);
            var addressId = await userAddressRepository.GetAddressIdsAsync(userAddress.UserId);

            Assert.Equal(addressId.FirstOrDefault(), userAddress.AddressId);
        }

        private UserAddresses CreateUserAddresses()
        {
            return new UserAddresses
            {
                AddressId = new Guid(),
                UserId = new Guid()
            };
        }

        private UserAddressesRepository GetInMemoryUserAddressesRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new UserAddressesRepository(toolshedContext);
        }
    }
}
