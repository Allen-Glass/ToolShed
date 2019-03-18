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

        public RentalService(ICardSQLService cardSQLService
            , ITenantSQLService tenantSQLService
            , IDispenserSQLService dispenserSQLService
            , IItemSQLService toolSQLService
            , IIotActionServices iotActionServices)
        {
            this.cardSQLService = cardSQLService;
            this.tenantSQLService = tenantSQLService;
            this.dispenserSQLService = dispenserSQLService;
            this.toolSQLService = toolSQLService;
            this.iotActionServices = iotActionServices;
        }

        /// <summary>
        /// User orders a rental
        /// </summary>
        /// <returns></returns>
        public async Task PlaceRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            await iotActionServices.InformDispenserOfActionAsync();
        }

        /// <summary>
        /// After the user picks up the item, start the rental of an item
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        public async Task StartRentalAsync(Guid rentalId)
        {
            //receive response from dispenser
            //charge for item
            //start timer
            //write record
        }
    }
}
