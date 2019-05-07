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
            await cardAddressRepository.AddCardAddressAsync(cardAddress);
        }

        [Fact]
        public async Task GetCardIdsByUserId()
        {
            await cardAddressRepository.AddCardAddressAsync(cardAddress);
            var addressIds = await cardAddressRepository.GetAddressAsync(cardAddress.CardId);

            Assert.Equal(addressIds.FirstOrDefault(), cardAddress.CardId);
        }

        private CardAddress CreateCardAddress()
        {
            return new CardAddress
            {
                AddressId = new Guid(),
                CardId = new Guid()
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
