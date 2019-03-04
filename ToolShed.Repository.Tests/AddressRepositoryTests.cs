using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Toolshed.Models.Enums;
using Toolshed.Models.User;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;
using Xunit;

namespace ToolShed.Repository.Tests
{
    public class AddressRepositoryTests
    {
        private readonly AddressRepository addressRepository;
        private readonly Address address;

        public AddressRepositoryTests()
        {
            addressRepository = GetInMemoryApplicantRepository();
            address = CreateAddress();
        }

        [Fact]
        public async Task AddAddress()
        {
            //await addressRepository.AddAddressAsync(address);
        }

        private Address CreateAddress()
        {
            return new Address
            {
                State = "NY",
                StreetName = "1111 Fools Road",
                AddressType = AddressType.user,
                AptNumber = "104",
                City = "NYC",
                Country = "USA",
                ZipCode = "98052",
                StreetName2 = string.Empty
            };
        }

        private AddressRepository GetInMemoryApplicantRepository()
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
