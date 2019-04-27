using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Repository.Context;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;
using ToolShed.Repository.Services;

namespace ToolShed.Repository.Tests.Services
{
    public class UserSQLServiceTests
    {
        private readonly IUserSQLService userSQLService;

        public UserSQLServiceTests()
        {
            userSQLService = CreateUserSQLService();
        }

        private IUserSQLService CreateUserSQLService()
        {
            return new UserSQLService(GetInMemoryUserRepository(),
                GetInMemoryAddressRepository(),
                GetInMemoryCardRepository(),
                GetInMemoryUserCardRepository(),
                GetInMemoryUserAddressesRepository(),
                GetInMemoryCardAddressesRepository());
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

        private CardRepository GetInMemoryCardRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new CardRepository(toolshedContext);
        }

        private UserCardRepository GetInMemoryUserCardRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new UserCardRepository(toolshedContext);
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

        private CardAddressRepository GetInMemoryCardAddressesRepository()
        {
            DbContextOptions<ToolShedContext> options;
            var builder = new DbContextOptionsBuilder<ToolShedContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;
            var toolshedContext = new ToolShedContext(options);
            toolshedContext.Database.EnsureDeleted();
            toolshedContext.Database.EnsureCreated();

            return new CardAddressRepository(toolshedContext);
        }
    }
}
