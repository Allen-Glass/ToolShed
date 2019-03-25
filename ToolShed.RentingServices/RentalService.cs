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
        private readonly RandomCodeGenerator randomCodeGenerator;

        public RentalService(IIotActionServices iotActionServices
            , IRentalSQLService rentalSQLService
            , RandomCodeGenerator randomCodeGenerator)
        {
            this.iotActionServices = iotActionServices;
            this.rentalSQLService = rentalSQLService;
            this.randomCodeGenerator = randomCodeGenerator;
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

        public async Task<Rental> CheckRentalStatusAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            return await rentalSQLService.GetRentalAsync(rentalId);
        }
    }
}
