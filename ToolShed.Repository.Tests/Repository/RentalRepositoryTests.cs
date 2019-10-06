using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;
using ToolShed.Repository.Repositories;
using Xunit;

namespace ToolShed.Repository.Tests.Repository
{
    public class RentalRepositoryTests
    {
        private readonly User newAccount;
        private readonly Rental rental;
        private readonly RentalRepository rentalRepository;

        public RentalRepositoryTests()
        {
            newAccount = CreateNewAccount();
            rental = CreateRental();
            rentalRepository = GetInMemoryRentalRepository();
        }

        [Fact]
        public async Task AddRentalAsync()
        {
            await rentalRepository.AddAsync(rental);
        }

        [Fact]
        public async Task GetRentalByRentalIdAsync()
        {
            var rentalId = await rentalRepository.AddAsync(rental);
            var dtoRental = await rentalRepository.GetAsync(rentalId);

            Assert.Equal(rental, dtoRental);
        }

        [Fact]
        public async Task GetRentalsByUserAsync()
        {
            var rentalId = await rentalRepository.AddAsync(rental);
            var dtoRentals = await rentalRepository.ListAsync(rental.UserId);

            Assert.Equal(rental, dtoRentals.FirstOrDefault());
        }

        [Fact]
        public async Task CheckLockerCodeAsync()
        {
            var rentalId = await rentalRepository.AddAsync(rental);
            var correctLockerCode = await rentalRepository.CheckLockerCodeAsync(rentalId, rental.LockerCode);

            Assert.True(correctLockerCode);
        }

        [Fact]
        public async Task CheckBadLockerCodeAsync()
        {
            var rentalId = await rentalRepository.AddAsync(rental);
            var correctLockerCode = await rentalRepository.CheckLockerCodeAsync(rental.UserId,"1111");

            Assert.False(correctLockerCode);
        }

        [Fact]
        public async Task CompleteRentalFromGuidAsync()
        {
            var rentalId = await rentalRepository.AddAsync(rental);
            await rentalRepository.CompleteRentalAsync(rentalId);
            var dtoRental = await rentalRepository.GetAsync(rentalId);

            Assert.True(dtoRental.HasBeenReturned);
        }

        [Fact]
        public async Task CompleteRentalAsync()
        {
            var rentalId = await rentalRepository.AddAsync(rental);
            await rentalRepository.CompleteRentalAsync(rentalId);
            var dtoRental = await rentalRepository.GetAsync(rentalId);

            Assert.True(dtoRental.HasBeenReturned);
        }

        private User CreateNewAccount()
        {
            return new User
            {
                Email = "testemail",
                Password = "password"
            };
        }

        private Rental CreateRental()
        {
            return new Rental
            {
                ReturnType = Models.Enums.ReturnType.ChargeFullPrice,
                HasBeenReturned = false,
                IsUserOwnedNow = false,
                ItemRentalDetailsId = Guid.NewGuid(),
                RentalDueTime = DateTime.MaxValue,
                RentalReturnTime = DateTime.UtcNow.Add(new TimeSpan(0, 45, 0)),
                RentalStartTime = DateTime.UtcNow,
                UserId = Guid.NewGuid(),
                LockerCode = "654321"
            };
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
    }
}
