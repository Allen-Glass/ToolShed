using System;
using Toolshed.Models.Enums;
using ToolShed.Models.API;

namespace ToolShed.Repository.Mapping
{
    public static class AddressMapping
    {

        public static Models.Repository.UserAddresses CreateUserAddressDTO(Guid userId, Guid addressId)
        {
            return new Models.Repository.UserAddresses
            {
                UserId = userId,
                AddressId = addressId
            };
        }

        public static Models.Repository.CardAddress CreateCardAddressDTO(Guid cardId, Guid addressId)
        {
            return new Models.Repository.CardAddress
            {
                AddressId = addressId,
                CardId = cardId
            };
        }

        public static Models.Repository.Address CreateDtoAddress(Address address)
        {
            return new Models.Repository.Address
            {
                AddressType = AddressType.User,
                AptNumber = address.AptNumber,
                City = address.City,
                Country = address.Country,
                State = address.State,
                StreetName = address.StreetName,
                StreetName2 = address.StreetName2,
                ZipCode = address.ZipCode
            };
        }

        public static Address ConvertDtoAddressToAddress(Models.Repository.Address address)
        {
            return new Address
            {
                AddressId = address.AddressId,
                AddressType = address.AddressType,
                AptNumber = address.AptNumber,
                City = address.City,
                Country = address.Country,
                State = address.State,
                StreetName = address.StreetName,
                StreetName2 = address.StreetName2,
                ZipCode = address.ZipCode
            };
        }
    }
}
