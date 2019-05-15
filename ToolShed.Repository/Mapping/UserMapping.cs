using System;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class UserMapping
    {
        public static Models.Repository.User CreateDtoUser(User user)
        {
            return new Models.Repository.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
        }

        public static Models.Repository.User CreateDtoUser(User user, Guid addressId)
        {
            return new Models.Repository.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AddressId = addressId,
                UserName = user.UserName
            };
        }

        public static User ConvertDtoUser(Models.Repository.User user)
        {
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.UserId,
                UserName = user.UserName
            };
        }
    }
}
