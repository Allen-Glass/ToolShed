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
    public class CardRepositoryTests
    {
        private readonly CardRepository cardRepository;
        private readonly Card card;

        public CardRepositoryTests()
        {
            cardRepository = GetInMemoryCardRepository();
            card = CreateNewCard();
        }

        [Fact]
        public async Task AddAddress()
        {
            await cardRepository.AddCardAsync(card);
        }

        [Fact]
        public async Task GetCardByUserId()
        {
            await cardRepository.AddCardAsync(card);

            var dtoCards = await cardRepository.GetCardsByUserIdAsync(card.UserId);
            var dtoCard = dtoCards.FirstOrDefault();

            Assert.Equal(card, dtoCard);
        }

        [Fact]
        public async Task GetCardByEmptyCardId()
        {
            var userId = Guid.Empty;

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                 await cardRepository.GetCardsByUserIdAsync(userId));
        }

        private Card CreateNewCard()
        {
            return new Card
            {
                BillingAddressId = new Guid("12345678-1234-1234-1234-123456789012"),
                CardHolderName = "John",
                CardNumber = "0987-9789-9873-0083",
                UserId = new Guid("12345678-1234-1234-1234-123456789012")
            };
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
    }
}
