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
        public async Task PlaceRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            //Create rental record
            await rentalSQLService.CreateNewRentalAsync(rental);
            
            //send dispenser action
            await iotActionServices.InformDispenserOfActionAsync("MAH_PIE", CreateActions(rental));
        }

        private Actions CreateActions(Rental rental)
        {
            return new Actions
            {
                ActionType = ActionType.sendcode,
                LockerCode = randomCodeGenerator.CreateLockerCombo().ToString(),
                LockerNumber = rental.Item.ItemLocker
            };
        }

        /// <summary>
        /// After the user picks up the item, start the rental of an item
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        public async Task StartRentalAsync(Guid rentalId)
        {

        }
    }
}
