using System;
using System.Threading.Tasks;
using ToolShed.IotHub.Interfaces;
using ToolShed.Models.API;
using ToolShed.Models.Enums;
using ToolShed.Models.IoTHub;
using ToolShed.Renting;
using ToolShed.RentingServices.Interfaces;
using ToolShed.Repository.Interfaces;

namespace ToolShed.RentingServices
{
    public class RentalService : IRentalService
    {
        private readonly IIotActionServices iotActionServices;
        private readonly IRentalSQLService rentalSQLService;
        private readonly IItemSQLService itemSQLService;
        private readonly RandomCodeGenerator randomCodeGenerator;

        public RentalService(IIotActionServices iotActionServices
            , IRentalSQLService rentalSQLService
            , RandomCodeGenerator randomCodeGenerator
            , IItemSQLService itemSQLService)
        {
            this.iotActionServices = iotActionServices;
            this.rentalSQLService = rentalSQLService;
            this.randomCodeGenerator = randomCodeGenerator;
            this.itemSQLService = itemSQLService;
        }

        /// <summary>
        /// User orders a rental
        /// </summary>
        /// <returns></returns>
        public async Task<Guid> PlaceRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            //Prepare action to be sent to dispenser
            var actions = CreateActions(rental);
            rental.LockerCode = randomCodeGenerator.CreateLockerCombo();

            //Create rental
            var rentalGuid = await rentalSQLService.CreateNewRentalAsync(rental);
            
            //send dispenser action
            await iotActionServices.InformDispenserOfActionAsync("MAH_PIE", actions);

            return rentalGuid;
        }

        private Actions CreateActions(Rental rental)
        {
            return new Actions
            {
                ActionType = ActionType.sendcode,
                LockerCode = rental.LockerCode,
                LockerNumber = rental.Item.ItemLocker
            };
        }

        /// <summary>
        /// After the user picks up the item, start the rental of an item
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        public async Task StartRentalAsync(Rental rental)
        {
            if (rental.RentalId == Guid.Empty)
                throw new ArgumentNullException();

            var workingLockerCode = await rentalSQLService.CheckLockerCodeAsync(rental);

            if (!workingLockerCode)
                throw new UnauthorizedAccessException();

            await iotActionServices.InformDispenserOfActionAsync("MAH_PIE", BeginRentalAction(rental));
        }

        private Actions BeginRentalAction(Rental rental)
        {
            return new Actions
            {
                ActionType = ActionType.unlock,
                LockerNumber = rental.Item.ItemLocker
            };
        }

        /// <summary>
        /// look to see rental status
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        public async Task<Rental> CheckRentalStatusAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            return await rentalSQLService.GetRentalAsync(rentalId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        public async Task CompleteRentalAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            await rentalSQLService.CompleteRentalAsync(rentalId);
        }

        /// <summary>
        /// figure out the users return time (do i really need this ugliness...)
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        private Rental DetermineFinalPrice(Rental rental)
        {
            var rentalOnTime = rental.RentalReturnTime <= rental.RentalDueTime;
            var rentalOverdue = !rentalOnTime;

            if (rentalOnTime)
            {
                rental.ReturnType = ReturnType.OnTime;
            }

            if (rentalOverdue)
            {
                var hasPaidFullPrice = rental.Item.BuyPrice < rental.Payment.TotalCost;
                if (hasPaidFullPrice)
                    rental.ReturnType = ReturnType.ChargeFullPrice;
                else
                    rental.ReturnType = ReturnType.Overdue;
            }

            return rental;
        }
    }
}
