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
    public class UserRepositoryTests
    {
        private readonly User newAccount;
        private readonly User userAccount;
        private readonly UserRepository userRepository;

        public UserRepositoryTests()
        {
            newAccount = CreateNewAccount();
            userAccount = CreateUser();
            userRepository = GetInMemoryUserRepository();
        }

        [Fact]
        public async Task AddUserAsync()
        {
            await userRepository.AddAsync(newAccount);
        }

        [Fact]
        public async Task ConfirmEmailDoesNotExist()
        {
            await userRepository.AddAsync(newAccount);
            var doesExist = await userRepository.CheckIfUserEmailExists(newAccount.Email);

            Assert.True(doesExist);
        }

        [Fact]
        public async Task ConfirmEmailExists()
        {
            await userRepository.AddAsync(newAccount);
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
            await userRepository.AddAsync(newAccount);
            var user = await userRepository.GetAsync(newAccount.Email);

            Assert.Equal(newAccount, user);
        }

        [Fact]
        public async Task GetUserByUserId()
        {
            var id = await userRepository.AddAsync(newAccount);
            var user = await userRepository.GetAsync(id);

            Assert.Equal(newAccount, user);
        }

        [Fact]
        public async Task GetAllUsers()
        {
            var id = await userRepository.AddAsync(newAccount);
            var user = await userRepository.ListAsync();

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

        private User CreateUser()
        {
            return new User
            {
                Email = "testemail",
                FirstName = "name",
                LastName = "last",
                Password = "password",
                AddressId = Guid.NewGuid()
            };
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
    }
}
