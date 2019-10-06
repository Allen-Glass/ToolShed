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
    public class CardAddressRepositoryTests
    {
        private readonly CardAddressRepository cardAddressRepository;
        private readonly CardAddress cardAddress;

        public CardAddressRepositoryTests()
        {
            cardAddressRepository = GetInMemoryCardAddressRepository();
            cardAddress = CreateCardAddress();
        }

        [Fact]
        public async Task AddUserCard()
        {
            await cardAddressRepository.AddAsync(cardAddress);
        }

        [Fact]
        public async Task GetCardAddress()
        {
            await cardAddressRepository.AddAsync(cardAddress);
            var addressIds = await cardAddressRepository.GetAsync(cardAddress.CardId);

            Assert.Equal(addressIds.FirstOrDefault(), cardAddress.AddressId);
        }

        private CardAddress CreateCardAddress()
        {
            return new CardAddress
            {
                AddressId = Guid.NewGuid(),
                CardId = Guid.NewGuid()
            };
        }

        private CardAddressRepository GetInMemoryCardAddressRepository()
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
