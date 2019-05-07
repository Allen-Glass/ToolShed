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
    public class UserCardRepositoryTests
    {
        private readonly UserCardRepository userCardRepository;
        private readonly UserCard userCard;

        public UserCardRepositoryTests()
        {
            userCardRepository = GetInMemoryUserCardRepository();
            userCard = CreateUserCard();
        }

        [Fact]
        public async Task AddUserCard()
        {
            await userCardRepository.AddUserCardAsync(userCard);
        }

        [Fact]
        public async Task GetCardIdsByUserId()
        {
            await userCardRepository.AddUserCardAsync(userCard);
            var cardIds = await userCardRepository.GetCardIdsAsync(userCard.UserId);

            Assert.Equal(cardIds.FirstOrDefault(), userCard.CardId);
        }

        private UserCard CreateUserCard()
        {
            return new UserCard
            {
                UserId = new Guid(),
                CardId = new Guid()
            };
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
    }
}
