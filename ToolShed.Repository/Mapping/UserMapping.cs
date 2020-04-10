using System;
using System.Collections.Generic;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class UserMapping
    {
        public static Models.Repository.User CreateDtoUser(this User user)
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

        public static Models.Repository.User CreateDtoUser(this User user, Guid addressId)
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

        public static User ConvertDtoUser(this Models.Repository.User user)
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

        public static IEnumerable<User> ConvertDtoUsers(this IEnumerable<Models.Repository.User> users)
        {
            var userList = new List<User>();
            foreach (var user in users)
            {
                userList.Add(ConvertDtoUser(user));
            }

            return userList;
        }
    }
}
