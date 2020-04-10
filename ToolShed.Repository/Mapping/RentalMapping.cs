using System;
using ToolShed.Models.API;
using ToolShed.Models.Enums;

namespace ToolShed.Repository.Mapping
{
    public static class RentalMapping
    {
        public static Models.Repository.Rental CreateDtoRental(this Rental rental)
        {
            return new Models.Repository.Rental
            {
                RentalStartTime = DateTime.UtcNow,
                RentalDueTime = rental.RentalDueTime,
                HasBeenReturned = false,
                IsUserOwnedNow = false,
                UserId = rental.User.UserId,
                LockerCode = rental.LockerCode
            };
        }

        public static Rental ConvertDtoRentalToRental(this Models.Repository.Rental rental)
        {
            return new Rental
            {
                RentalStartTime = rental.RentalStartTime,
                RentalDueTime = rental.RentalDueTime,
                RentalId = rental.RentalId,
                RentalReturnTime = rental.RentalReturnTime,
                HasBeenReturned = rental.HasBeenReturned,
                IsUserOwnedNow = rental.IsUserOwnedNow,
                LockerCode = rental.LockerCode
            };
        }

        public static Models.Repository.RentalRecord CreateDtoRentalRecord(this Rental rental)
        {
            return new Models.Repository.RentalRecord
            {
                Action = ($"{rental.ItemRentalDetails.Item.DisplayName} rental started at {rental.RentalStartTime}."),
                ActionType = RentalAction.confirm,
                RentalId = rental.RentalId,
                UserId = rental.User.UserId
            };
        }

        public static Models.Repository.RentalRecord CreateCompleteDtoRentalRecord(this Models.Repository.Rental rental)
        {
            return new Models.Repository.RentalRecord
            {
                Action = ($"{rental.RentalId} rental returned at {DateTime.UtcNow}."),
                ActionType = RentalAction.returned,
                RentalId = rental.RentalId,
                UserId = rental.UserId
            };
        }

        public static Models.Repository.RentalRecord CreateFullPriceDtoRentalRecord(this Models.Repository.Rental rental)
        {
            return new Models.Repository.RentalRecord
            {
                Action = ($"Rental {rental.RentalId} was not returned in a timely manner. You have been charged the full price."),
                ActionType = RentalAction.returned,
                RentalId = rental.RentalId,
                UserId = rental.UserId
            };
        }

        public static Models.Repository.RentalRecord CreateSuccessfulLockerCodeRecord(this Rental rental)
        {
            return new Models.Repository.RentalRecord
            {
                Action = ($"Rental {rental.RentalId}, successfully. {rental.User.FirstName} {rental.User.LastName} has unlocked {rental.ItemRentalDetails.Item.DisplayName}."),
                ActionType = RentalAction.correctCode,
                RentalId = rental.RentalId,
                UserId = rental.User.UserId
            };
        }

        public static Models.Repository.RentalRecord CreateFailingLockerCodeRecord(this Rental rental)
        {
            return new Models.Repository.RentalRecord
            {
                Action = ($"Rental {rental.RentalId}, failed with {rental.LockerCode}. {rental.User.FirstName} {rental.User.LastName} has unlocked {rental.ItemRentalDetails.Item.DisplayName}."),
                ActionType = RentalAction.badCode,
                RentalId = rental.RentalId,
                UserId = rental.User.UserId
            };
        }
    }
}
