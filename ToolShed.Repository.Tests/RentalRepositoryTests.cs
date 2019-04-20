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

namespace ToolShed.Repository.Tests
{
    public class RentalRepositoryTests
    {
        private readonly User newAccount;
        private readonly User userAccount;
        private readonly UserRepository userRepository;

        public RentalRepositoryTests()
        {
            newAccount = CreateNewAccount();
        }

        [Fact]
        public async Task AddUserAsync()
        {
            await userRepository.AddUserAsync(newAccount);
        }

        [Fact]
        public async Task ConfirmEmailDoesNotExist()
        {
            await userRepository.AddUserAsync(newAccount);
            var doesExist = await userRepository.CheckIfUserEmailExists(newAccount.Email);

            Assert.True(doesExist);
        }

        [Fact]
        public async Task ConfirmEmailExists()
        {
            await userRepository.AddUserAsync(newAccount);
            var doesExist = await userRepository.CheckIfUserEmailExists("things");

            Assert.False(doesExist);
        }

        [Fact]
        public async Task CheckUserExistEmptyEmail()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => userRepository.CheckIfUserEmailExists(""));
        }

        [Fact]
        public async Task GetUserByEmail()
        {
            await userRepository.AddUserAsync(newAccount);
            var user = await userRepository.GetUserByEmailAsync(newAccount.Email);

            Assert.Equal(newAccount, user);
        }

        [Fact]
        public async Task GetUserByUserId()
        {
            var id = await userRepository.AddUserAsync(newAccount);
            var user = await userRepository.GetUserByUserIdAsync(id);

            Assert.Equal(newAccount, user);
        }

        [Fact]
        public async Task GetAllUsers()
        {
            var id = await userRepository.AddUserAsync(newAccount);
            var user = await userRepository.GetAllUsers();

            Assert.Equal(newAccount, user.FirstOrDefault());
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
