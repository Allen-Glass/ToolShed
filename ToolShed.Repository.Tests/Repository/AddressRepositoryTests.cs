using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;
using Xunit;

namespace ToolShed.Repository.Tests.Repository
{
    public class AddressRepositoryTests
    {
        private readonly AddressRepository addressRepository;
        private readonly Address address;
        private readonly IEnumerable<Address> addresses;
        private readonly Guid fakeGuid;

        public AddressRepositoryTests()
        {
            addressRepository = GetInMemoryAddressRepository();
            address = CreateAddress();
            addresses = CreateAddresses();
            fakeGuid = CreateFakeGuid();
        }

        [Fact]
        public async Task AddAddress()
        {
            await addressRepository.AddAsync(address);
        }

        [Fact]
        public async Task GetAddressByAddressId()
        {
            var addressId = await addressRepository.AddAsync(address);

            var dtoAddress = await addressRepository.GetAsync(addressId);

            Assert.Equal(address, dtoAddress);
        }

        [Fact]
        public async Task GetAddressByEmptyAddressId()
        {
            var addressId = Guid.Empty;

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                 await addressRepository.GetAsync(addressId));
        }

        [Fact]
        public async Task GetNonexistentAddressByAddressId()
        {
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
                 await addressRepository.GetAsync(fakeGuid));
        }

        [Fact]
        public async Task GetAddressesByState()
        {
            var addressId = addressRepository.AddAsync(address);

            var dtoAddress = await addressRepository.GetAddressesByStateAsync("NY");
            var firstAddress = dtoAddress.FirstOrDefault();

            Assert.Equal(address.State, firstAddress.State);
        }

        [Fact]
        public async Task GetAddressesByCity()
        {
            var addressId = addressRepository.AddAsync(address);

            var dtoAddress = await addressRepository.GetAddressByCityAsync("NYC");
            var firstAddress = dtoAddress.FirstOrDefault();

            Assert.Equal(address.City, firstAddress.City);
        }

        [Fact]
        public async Task GetAddressesByZip()
        {
            var addressId = addressRepository.AddAsync(address);

            var dtoAddress = await addressRepository.GetAddressByZipCodeAsync("46879");
            var firstAddress = dtoAddress.FirstOrDefault();

            Assert.Equal(address.ZipCode, firstAddress.ZipCode);
        }

        private Address CreateAddress()
        {
            return new Address
            {
                State = "NY",
                StreetName = "1111 Fools Road",
                AddressType = AddressType.User,
                AptNumber = "104",
                City = "NYC",
                Country = "USA",
                ZipCode = "46879",
                StreetName2 = string.Empty
            };
        }

        private IEnumerable<Address> CreateAddresses()
        {
            return new List<Address>
            {
                CreateAddress(),
                CreateAddress(),
                CreateAddress()
            };
        }

        private Guid CreateFakeGuid()
        {
            return new Guid("12345678-1234-1234-1234-123456789012");
        }

        private AddressRepository GetInMemoryAddressRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new AddressRepository(toolshedContext);
        }
    }
}
