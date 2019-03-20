using System;
using ToolShed.Models.API;
using ToolShed.Models.Enums;

namespace ToolShed.Repository.Mapping
{
    public static class RentalMapping
    {
        public static Models.Repository.Rental CreateDtoRental(Rental rental)
        {
            return new Models.Repository.Rental
            {
                RentalStart = DateTime.UtcNow,
                RentalDue = rental.RentalDue,
                HasBeenReturned = false,
                IsUserOwnedNow = false,
                UserId = rental.User.UserId
            };
        }

        public static Models.Repository.RentalRecord CreateDtoRentalRecord(Rental rental)
        {
            return new Models.Repository.RentalRecord
            {
                Action = ($"{rental.Item.ItemName} rental started at {rental.RentalStart}."),
                ActionType = RentalAction.confirm,
                RentalId = rental.RentalId,
                UserId = rental.User.UserId
            };
        }

        public static Models.Repository.RentalRecord CreateCompleteDtoRentalRecord(Models.Repository.Rental rental)
        {
            return new Models.Repository.RentalRecord
            {
                Action = ($"{rental.RentalId} rental returned at {DateTime.UtcNow}."),
                ActionType = RentalAction.returned,
                RentalId = rental.RentalId,
                UserId = rental.UserId
            };
        }

        public static Models.Repository.RentalRecord CreateFullPriceDtoRentalRecord(Models.Repository.Rental rental)
        {
            return new Models.Repository.RentalRecord
            {
                Action = ($"Rental {rental.RentalId} was not returned in a timely manner. You have been charged the full price."),
                ActionType = RentalAction.returned,
                RentalId = rental.RentalId,
                UserId = rental.UserId
            };
        }
    }
}
