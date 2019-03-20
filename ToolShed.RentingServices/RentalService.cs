using System;
using System.Threading.Tasks;
using ToolShed.IotHub.Interfaces;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;

namespace ToolShed.RentingServices
{
    public class RentalService
    {
        private readonly ITenantSQLService tenantSQLService;
        private readonly ICardSQLService cardSQLService;
        private readonly IDispenserSQLService dispenserSQLService;
        private readonly IItemSQLService toolSQLService;
        private readonly IIotActionServices iotActionServices;
        private readonly IRentalSQLService rentalSQLService;

        public RentalService(ICardSQLService cardSQLService
            , ITenantSQLService tenantSQLService
            , IDispenserSQLService dispenserSQLService
            , IItemSQLService toolSQLService
            , IIotActionServices iotActionServices
            , IRentalSQLService rentalSQLService)
        {
            this.cardSQLService = cardSQLService;
            this.tenantSQLService = tenantSQLService;
            this.dispenserSQLService = dispenserSQLService;
            this.toolSQLService = toolSQLService;
            this.iotActionServices = iotActionServices;
            this.rentalSQLService = rentalSQLService;
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
            await iotActionServices.InformDispenserOfActionAsync();
        }

        /// <summary>
        /// After the user picks up the item, start the rental of an item
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        public async Task StartRentalAsync(Guid rentalId)
        {
            //charge for item
            //start timer by adding it to timed table
            //write record
            await rentalSQLService.CreateNewRentalAsync();
        }
    }
}
