using System;
using System.Collections.Generic;
using System.Text;
using Toolshed.Models.Enums;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class UserMapping
    {
        public static Models.Repository.User ConvertUserToDtoUser(User user)
        {
            return new Models.Repository.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static Models.Repository.User ConvertUserToDtoUser(User user, Guid addressId)
        {
            return new Models.Repository.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AddressId = addressId
            };
        }
    }
}
