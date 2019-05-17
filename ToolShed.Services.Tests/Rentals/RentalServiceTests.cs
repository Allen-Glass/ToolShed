using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using ToolShed.IotHub.Interfaces;
using ToolShed.Models.API;
using ToolShed.Models.Enums;
using ToolShed.Repository.Context;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;
using ToolShed.Repository.Services;
using ToolShed.Services;
using ToolShed.Services.Interfaces;
using ToolShed.Services.Rentals;
using Xunit;

namespace ToolShed.Renting.Tests.Rentals
{
    public class RentalServiceTests
    {
        private readonly IRentingService rentingService;
        private readonly Guid dispenserId = Guid.NewGuid();
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
        public async Task UseFailingLockerCode()
        {
            var rentalId = await rentingService.PlaceRentalAsync(rental);
            var fakeLockerCode = "123456";
            var rentalPickup = new Rental
            {
                RentalId = rentalId,
                LockerCode = fakeLockerCode
            };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                 await rentingService.StartRentalAsync(rentalPickup));
        }

        [Fact]
        public async Task UseSuccesfulLockerCode_ToStartRental()
        {
            var rentalId = await rentingService.PlaceRentalAsync(rental);
            rental.RentalId = rentalId;
            await rentingService.StartRentalAsync(rental);
        }

        [Fact]
        public async Task ReturnItemToDispenser()
        {
            var rentalId = await rentingService.PlaceRentalAsync(rental);
            rental.RentalId = rentalId;
            await rentingService.StartRentalAsync(rental);
            await rentingService.CompleteRentalAsync(rentalId);
        }

        private Rental CreateRental()
        {
            return new Rental
            {
                ReturnType = ReturnType.ChargeFullPrice,
                HasBeenReturned = false,
                IsUserOwnedNow = false,
                ItemRentalDetails = CreateItemRentalDetails(),
                RentalDueTime = DateTime.MaxValue,
                RentalReturnTime = DateTime.UtcNow.Add(new TimeSpan(0, 45, 0)),
                RentalStartTime = DateTime.UtcNow,
                User = CreateUser(),
                LockerCode = "654321",
                DispenserId = dispenserId
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
                DispenserId = dispenserId,
                ItemId = Guid.NewGuid(),
                TenantId = Guid.NewGuid()
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
                UserId = Guid.NewGuid()
            };
        }

        private IRentingService CreateRentalService()
        {
            return new RentingService(CreateIotActionServices(),
                CreateRentalSQLService(), 
                CreateDispenserService(),
                CreatePaymentService(),
                new RandomCodeGenerator(6));
        }

        private IDispenserService CreateDispenserService()
        {
            var moqDispenserService = new Mock<IDispenserService>();
            moqDispenserService.Setup(c => c.GetDispenserIotNameAsync(dispenserId)).ReturnsAsync("MUH_PIE");

            return moqDispenserService.Object;
        }

        private IIotActionServices CreateIotActionServices()
        {
            var moqIotActionServices = new Mock<IIotActionServices>();
            moqIotActionServices.Setup(c => c.InformDispenserOfActionAsync("MUH_PIE", CreateActions(CreateRental())));

            return moqIotActionServices.Object;
        }

        private Actions CreateActions(Rental rental)
        {
            return new Actions
            {
                ActionType = ActionType.sendcode,
                LockerCode = rental.LockerCode,
                LockerNumber = rental.ItemRentalDetails.LockerNumber
            };
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

        private IRentalDataService CreateRentalSQLService()
        {
            return new RentalDataService(GetInMemoryRentalRepository(),
                GetInMemoryUserRepository(), 
                GetInMemoryRentalRecordsRepository(),
                GetInMemoryDispenserRepository(),
                GetInMemoryItemRepository(),
                GetInMemoryItemRentalDetailsRepository());
        }

        private ITaxesDataService CreateTaxesSqlService()
        {
            return new TaxesDataService(CreateStateSalesTaxRepository());
        }

        private IItemDataService CreateItemSqlService()
        {
            return new ItemDataService(CreateItemRepository(),
                CreateItemBundleRepository(),
                CreateItemBundleMappingRepository());
        }

        private IDispenserDataService CreateDispenserSqlService()
        {
            return new DispenserDataService(GetInMemoryDispenserRepository(),
                GetInMemoryItemRepository(),
                GetInMemoryAddressRepository());
        }

        private ItemRepository CreateItemRepository()
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

        private ItemBundleRepository CreateItemBundleRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new ItemBundleRepository(toolshedContext);
        }

        private ItemBundleMappingRepository CreateItemBundleMappingRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new ItemBundleMappingRepository(toolshedContext);
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