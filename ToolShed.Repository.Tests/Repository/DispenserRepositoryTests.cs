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
    public class DispenserRepositoryTests
    {
        private readonly DispenserRepository dispenserRepository;
        private readonly Dispenser dispenser;

        public DispenserRepositoryTests()
        {
            dispenserRepository = GetInMemoryDispenserRepository();
            dispenser = CreateDispenser();
        }

        [Fact]
        public async Task AddDispenser()
        {
            await dispenserRepository.AddDispenserAsync(dispenser);
        }

        [Fact]
        public async Task GetAllDispenser()
        {
            await dispenserRepository.AddDispenserAsync(dispenser);
            var dtoDispenser = await dispenserRepository.GetAllDispensersAsync();

            Assert.Equal(dispenser, dtoDispenser.FirstOrDefault());
        }

        [Fact]
        public async Task GetDispenserByDispenserIdAsync()
        {
            var dispenserId = await dispenserRepository.AddDispenserAsync(dispenser);
            var dtoDispenser = await dispenserRepository.GetDispenserByDispenserIdAsync(dispenserId);

            Assert.Equal(dispenser, dtoDispenser);
        }

        [Fact]
        public async Task GetDispenserAddressIdAsync()
        {
            var dispenserId = await dispenserRepository.AddDispenserAsync(dispenser);
            var dispenserAddress = await dispenserRepository.GetDispenserAddressIdAsync(dispenserId);

            Assert.Equal(dispenser.DispenserAddressId, dispenserAddress);
        }

        [Fact]
        public async Task GetDispenserIotNameAsync()
        {
            var dispenserId = await dispenserRepository.AddDispenserAsync(dispenser);
            var dispenserName = await dispenserRepository.GetDispenserIotNameAsync(dispenserId);

            Assert.Equal(dispenser.DispenserIotName, dispenserName);
        }

        private Dispenser CreateDispenser()
        {
            return new Dispenser
            {
                CreationDate = DateTime.UtcNow,
                DecommissionDate = DateTime.UtcNow,
                DispenserAddressId = Guid.NewGuid(),
                DispenserIotName = "MAH_PIE",
                DispenserName = "LeSpenser",
                LastMaintenanceCheckDate = DateTime.UtcNow
            };
        }

        private DispenserRepository GetInMemoryDispenserRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new DispenserRepository(toolshedContext);
        }
    }
}
