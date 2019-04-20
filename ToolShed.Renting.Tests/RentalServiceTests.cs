using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ToolShed.IotHub.Interfaces;
using ToolShed.IotHub.Services;
using ToolShed.Models.API;
using ToolShed.Payments;
using ToolShed.Payments.Interfaces;
using ToolShed.Renting.Interfaces;
using ToolShed.Repository.Context;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;
using ToolShed.Repository.Services;
using Xunit;

namespace ToolShed.Renting.Tests
{
    public class RentalServiceTests
    {
        private readonly IRentingService rentingService;
        private readonly Rental rental;

        public RentalServiceTests()
        {
            rentingService = CreateRentalService();
            rental = CreateRental();
        }

        [Fact]
        public async Task RentItem()
        {
            await rentingService.PlaceRentalAsync(rental);
        }

        [Fact]
        public async Task UseSuccessfulLockerCode()
        {
            var rentalId = await rentingService.PlaceRentalAsync(rental);
            var userRental = await rentingService.CheckRentalStatusAsync(rentalId);
            userRental.User = CreateUser();
            userRental.ItemRentalDetails = CreateItemRentalDetails();

            await rentingService.StartRentalAsync(userRental);
        }

        [Fact]
        public async Task UseFailingLockerCode()
        {
            var rentalId = await rentingService.PlaceRentalAsync(rental);
            var fakeLockerCode = "123456";
            var rentalPickup = new Rental
            {
                RentalId = rentalId,
                LockerCode = fakeLockerCode
            };

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
                 await rentingService.StartRentalAsync(rentalPickup));
        }

        private Rental CreateRental()
        {
            return new Rental
            {
                ReturnType = Models.Enums.ReturnType.ChargeFullPrice,
                HasBeenReturned = false,
                IsUserOwnedNow = false,
                ItemRentalDetails = CreateItemRentalDetails(),
                RentalDueTime = DateTime.MaxValue,
                RentalReturnTime = DateTime.UtcNow.Add(new TimeSpan(0, 45, 0)),
                RentalStartTime = DateTime.UtcNow,
                User = CreateUser(),
                LockerCode = "654321"
            };
        }

        private ItemRentalDetails CreateItemRentalDetails()
        {
            return new ItemRentalDetails
            {
                BaseRentalFee = 1.0,
                Item = CreateItem(),
                LockerNumber = "4A",
                PricePerHour = 1.0
            };
        }

        private Item CreateItem()
        {
            return new Item
            {
                BuyPrice = 12.00,
                IsAvailable = true,
                IsDamaged = false,
                IsRentable = false,
                SalePrice = 12.00,
                DisplayName = "The Waxanator",
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

        private IRentingService CreateRentalService()
        {
            return new RentingService(CreateIotActionServices(),
                CreateRentalSQLService(), 
                null,
                null,
                null);
        }

        private IIotActionServices CreateIotActionServices()
        {
            var serviceClient = ServiceClient.CreateFromConnectionString("HostName=toolshed-hub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=0rV4SLwfduFh0N3xB5fNzZ0/gLa88Qjohr3r9D+yVkw=");
            return new IotActionServices(serviceClient);
        }

        private IPaymentService CreatePaymentService()
        {
            return new PaymentService(CreateTaxService(),
                CreateRentalSQLService());
        }

        private ITaxService CreateTaxService()
        {
            return new TaxService(CreateTaxesSqlService());
        }

        private IRentalSQLService CreateRentalSQLService()
        {
            return new RentalSQLService(GetInMemoryRentalRepository(),
                GetInMemoryUserRepository(), 
                GetInMemoryRentalRecordsRepository(),
                GetInMemoryDispenserRepository(),
                GetInMemoryItemRepository(),
                GetInMemoryItemRentalDetailsRepository());
        }

        private ITaxesSQLService CreateTaxesSqlService()
        {
            return new TaxesSQLService(CreateStateSalesTaxRepository());
        }

        private StateSalesTaxRepository CreateStateSalesTaxRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new StateSalesTaxRepository(toolshedContext);
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

        private ItemRentalDetailsRepository GetInMemoryItemRentalDetailsRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new ItemRentalDetailsRepository(toolshedContext);
        }
    }
}
