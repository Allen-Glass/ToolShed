using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ToolShed.IotHub.Interfaces;
using ToolShed.IotHub.Services;
using ToolShed.Models.API;
using ToolShed.RentingServices;
using ToolShed.RentingServices.Interfaces;
using ToolShed.Repository.Context;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;
using ToolShed.Repository.Services;
using Xunit;

namespace ToolShed.Renting.Tests
{
    public class RentalServiceTests
    {
        private readonly IRentalService rentalService;
        private readonly Rental rental;

        public RentalServiceTests()
        {
            rentalService = CreateRentalService();
            rental = CreateRental();
        }

        [Fact]
        public async Task RentItem()
        {
            await rentalService.PlaceRentalAsync(rental);
        }

        [Fact]
        public async Task UseSuccessfulLockerCode()
        {
            var rentalId = await rentalService.PlaceRentalAsync(rental);
            var userRental = await rentalService.CheckRentalStatusAsync(rentalId);
            userRental.User = CreateUser();
            userRental.Item = CreateItem();

            await rentalService.StartRentalAsync(userRental);
        }

        [Fact]
        public async Task UseFailingLockerCode()
        {
            var rentalId = await rentalService.PlaceRentalAsync(rental);
            var fakeLockerCode = "123456";
            var rentalPickup = new Rental
            {
                RentalId = rentalId,
                LockerCode = fakeLockerCode
            };

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
                 await rentalService.StartRentalAsync(rentalPickup));
        }

        private Rental CreateRental()
        {
            return new Rental
            {
                FinalCost = 5.00,
                HasBeenReturned = false,
                IsUserOwnedNow = false,
                Item = CreateItem(),
                RentalDue = DateTime.MaxValue,
                RentalReturned = DateTime.UtcNow.Add(new TimeSpan(0, 45, 0)),
                RentalStart = DateTime.UtcNow,
                User = CreateUser(),
                LockerCode = "654321"
            };
        }

        private Item CreateItem()
        {
            return new Item
            {
                BaseRentalPeriodDuration = 4.5,
                BuyPrice = 12.00,
                IsAvailable = true,
                IsDamaged = false,
                IsRentable = false,
                SalePrice = 12.00,
                ItemName = "The Waxanator",
                PricePerHourOver = 1.40,
                PricePerHour = 2.50,
                DispenserId = new Guid("12345678-1234-1234-1234-123456789012"),
                ItemId = new Guid("12345678-1234-1234-1234-123456789012"),
                TenantId = new Guid("12345678-1234-1234-1234-123456789012")
            };
        }

        private User CreateUser()
        {
            return new User
            {
                Address = null,
                CreditCards = null,
                Email = "testuser@gmail.com",
                FirstName = "allen",
                LastName = "glass",
                UserId = new Guid("12345678-1234-1234-1234-123456789012")
            };
        }

        private IRentalService CreateRentalService()
        {
            return new RentalService(CreateIotActionServices()
                , CreateRentalSQLService(), new RandomCodeGenerator(6));
        }

        private IIotActionServices CreateIotActionServices()
        {
            var serviceClient = ServiceClient.CreateFromConnectionString("HostName=toolshed-hub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=0rV4SLwfduFh0N3xB5fNzZ0/gLa88Qjohr3r9D+yVkw=");
            return new IotActionServices(serviceClient);
        }

        private IRentalSQLService CreateRentalSQLService()
        {
            return new RentalSQLService(GetInMemoryRentalRepository(), GetInMemoryUserRepository(), GetInMemoryRentalRecordsRepository());
        }

        private RentalRepository GetInMemoryRentalRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new RentalRepository(toolshedContext);
        }

        private UserRepository GetInMemoryUserRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new UserRepository(toolshedContext);
        }

        private RentalRecordsRepository GetInMemoryRentalRecordsRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new RentalRecordsRepository(toolshedContext);
        }
    }
}
