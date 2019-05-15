using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.Repository;
using ToolShed.Repository.Mapping;
using Xunit;

namespace ToolShed.Repository.Tests.Mapping
{
    public class UserMappingTests
    {
        private readonly Models.API.User user;
        private readonly User dtoUser;
        private readonly Models.API.Card card;
        private readonly Card dtoCard;
        private Guid addressId = Guid.NewGuid();
        private Guid userId = Guid.NewGuid();

        public UserMappingTests()
        {
            user = CreateUser();
            dtoUser = CreateUserWithUserId();
        }

        [Fact]
        public void CreateDtoUser()
        {
            var dtoUser = UserMapping.CreateDtoUser(user);

            Assert.IsType<User>(dtoUser);
            Assert.NotNull(dtoUser.Email);
            Assert.NotNull(dtoUser.FirstName);
            Assert.NotNull(dtoUser.LastName);
            Assert.NotNull(dtoUser.UserName);
            Assert.NotNull(dtoUser.Password);
        }

        [Fact]
        public void CreateDtoUserTwoParameters()
        {
            var dtoUser = UserMapping.CreateDtoUser(user, addressId);

            Assert.IsType<User>(dtoUser);
            Assert.Equal(addressId, dtoUser.AddressId);
            Assert.NotNull(dtoUser.Email);
            Assert.NotNull(dtoUser.FirstName);
            Assert.NotNull(dtoUser.LastName);
            Assert.NotNull(dtoUser.UserName);
            Assert.NotNull(dtoUser.Password);
        }

        [Fact]
        public void ConvertDtoUser()
        {
            var user = UserMapping.ConvertDtoUser(dtoUser);

            Assert.IsType<Models.API.User>(user);
            Assert.Equal(user.UserId, userId);
            Assert.NotNull(user.Email);
            Assert.NotNull(user.FirstName);
            Assert.NotNull(user.LastName);
            Assert.NotNull(user.UserName);
        }

        private User CreateUserWithUserId()
        {
            return new User
            {
                UserId = userId,
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
                LastName = "doe",
                Password = ";234kj;ldsf"
            };
        }
    }
}
