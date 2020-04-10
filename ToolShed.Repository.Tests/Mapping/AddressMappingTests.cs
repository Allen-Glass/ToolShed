using System;
using Toolshed.Models.Enums;
using ToolShed.Models.Repository;
using ToolShed.Repository.Mapping;
using Xunit;

namespace ToolShed.Repository.Tests.Mapping
{
    public class AddressMappingTests
    {
        private readonly Address dtoAddress;
        private readonly User user;
        private readonly Card card;
        private readonly Models.API.Address address;
        private Guid addressId = Guid.NewGuid();

        public AddressMappingTests()
        {
            dtoAddress = CreateAddressWithAddressId();
            user = CreateUserWithUserId();
            card = CreateCardWithCardId();
            address = CreateAddress();
        }

        [Fact]
        public void CreateUserAddressDTO()
        {
            var userAddress = user.UserId.CreateUserAddressDTO(dtoAddress.AddressId);

            Assert.IsType<UserAddresses>(userAddress);
            Assert.NotNull(userAddress);
            Assert.Equal(dtoAddress.AddressId, userAddress.AddressId);
            Assert.Equal(user.UserId, userAddress.UserId);
        }

        [Fact]
        public void CreateCardAddressDTO()
        {
            var cardAddress = AddressMapping.CreateCardAddressDTO(card.CardId, dtoAddress.AddressId);

            Assert.IsType<CardAddress>(cardAddress);
            Assert.NotNull(cardAddress);
            Assert.Equal(dtoAddress.AddressId, cardAddress.AddressId);
            Assert.Equal(card.CardId, cardAddress.CardId);
        }

        [Fact]
        public void CreateDtoAddress()
        {
            var dtoAddress = AddressMapping.CreateDtoAddress(address);

            Assert.IsType<Address>(dtoAddress);
            Assert.Equal(AddressType.User, dtoAddress.AddressType);
            Assert.NotNull(dtoAddress.AptNumber);
            Assert.NotNull(dtoAddress.City);
            Assert.NotNull(dtoAddress.Country);
            Assert.NotNull(dtoAddress.State);
            Assert.NotNull(dtoAddress.StreetName);
            Assert.NotNull(dtoAddress.StreetName2);
            Assert.NotNull(dtoAddress.ZipCode);
        }

        [Fact]
        public void ConvertDtoAddressToAddress()
        {
            var address = AddressMapping.ConvertDtoAddressToAddress(dtoAddress);

            Assert.IsType<Models.API.Address>(address);
            Assert.Equal(dtoAddress.AddressId, address.AddressId);
            Assert.Equal(AddressType.User, dtoAddress.AddressType);
            Assert.NotNull(dtoAddress.AptNumber);
            Assert.NotNull(dtoAddress.City);
            Assert.NotNull(dtoAddress.Country);
            Assert.NotNull(dtoAddress.State);
            Assert.NotNull(dtoAddress.StreetName);
            Assert.NotNull(dtoAddress.StreetName2);
            Assert.NotNull(dtoAddress.ZipCode);
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

        private Address CreateAddressWithAddressId()
        {
            return new Address
            {
                AddressId = addressId,
                AddressType = AddressType.User,
                AptNumber = "12",
                City = "nyc",
                State = "ny",
                StreetName = "123 some place",
                StreetName2 = string.Empty,
                Country = "USA",
                ZipCode = "12345"
            };
        }

        private Models.API.Address CreateAddress()
        {
            return new Models.API.Address
            {
                AddressType = AddressType.User,
                AptNumber = "12",
                City = "nyc",
                State = "ny",
                StreetName = "123 some place",
                StreetName2 = string.Empty,
                Country = "USA",
                ZipCode = "12345"
            };
        }

        private Models.Repository.User CreateUserWithUserId()
        {
            return new Models.Repository.User
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
    }
}
