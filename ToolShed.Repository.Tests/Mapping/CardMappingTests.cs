using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.Repository;
using ToolShed.Repository.Mapping;
using Xunit;

namespace ToolShed.Repository.Tests.Mapping
{
    public class CardMappingTests
    {
        private readonly Models.API.User user;
        private readonly User dtoUser;
        private readonly Models.API.Card card;
        private readonly Card dtoCard;
        private Guid addressId = Guid.NewGuid();

        public CardMappingTests()
        {
            user = CreateUser();
            dtoUser = CreateUserWithUserId();
            card = CreateCard();
            dtoCard = CreateCardWithCardId();
        }

        [Fact]
        public void CreateUserCardDTO()
        {
            var userCard = CardMapping.CreateUserCardDTO(dtoUser.UserId, dtoCard.CardId);

            Assert.IsType<UserCard>(userCard);
            Assert.NotNull(userCard);
            Assert.Equal(dtoCard.CardId, userCard.CardId);
            Assert.Equal(dtoUser.UserId, userCard.UserId);
        }

        [Fact]
        public void CreateUserCardDTOFromCard()
        {
            card.CardId = Guid.NewGuid();
            card.UserId = Guid.NewGuid();
            var userCard = CardMapping.CreateUserCardDTO(card);

            Assert.IsType<UserCard>(userCard);
            Assert.NotNull(userCard);
            Assert.Equal(card.UserId, userCard.UserId);
            Assert.Equal(card.CardId, userCard.CardId);
        }

        [Fact]
        public void CreateDtoAddress()
        {
            var dtoCard = CardMapping.CreateDtoCard(card);

            Assert.IsType<Card>(dtoCard);
            Assert.NotNull(dtoCard.CCV);
            Assert.NotNull(dtoCard.CardNumber);
            Assert.NotNull(dtoCard.CardHolderName);
        }

        [Fact]
        public void ConvertDtoAddressToAddress()
        {
            var card = CardMapping.ConvertDtoCardToCard(dtoCard);

            Assert.IsType<Models.API.Card>(card);
            Assert.Equal(dtoCard.UserId, card.UserId);
            Assert.NotNull(card.CardHolderName);
            Assert.NotNull(card.CCV);
            Assert.NotNull(card.CardNumber);
        }

        private Card CreateCardWithCardId()
        {
            return new Models.Repository.Card
            {
                CardId = Guid.NewGuid(),
                CardHolderName = "john",
                CardNumber = "124123-234120-23423",
                CCV = "111",
                AddressId = addressId,
                UserId = Guid.NewGuid()
            };
        }

        private Models.API.Card CreateCard()
        {
            return new Models.API.Card
            {
                BillingAddress = new Models.API.Address(),
                CardHolderName = "john",
                CardNumber = "124123-234120-23423",
                CCV = "111"
            };
        }

        private User CreateUserWithUserId()
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                UserName = "johnledoe",
                AddressId = addressId,
                Email = "jdoe@contoso.org",
                FirstName = "john",
                LastName = "doe",
                Password = "flkjasdol;fujioasnn"
            };
        }

        private Models.API.User CreateUser()
        {
            return new Models.API.User
            {
                UserName = "johnledoe",
                Email = "jdoe@contoso.org",
                FirstName = "john",
                LastName = "doe"
            };
        }
    }
}
