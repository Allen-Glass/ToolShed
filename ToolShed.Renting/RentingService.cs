using System;
using System.Threading.Tasks;
using ToolShed.IotHub.Interfaces;
using ToolShed.Models.API;
using ToolShed.Models.Enums;
using ToolShed.Renting.Interfaces;
using ToolShed.Repository.Interfaces;
using ToolShed.Services.Interfaces;

namespace ToolShed.Renting
{
    public class RentingService : IRentingService
    {
        private readonly IIotActionServices iotActionServices;
        private readonly IRentalSQLService rentalSQLService;
        private readonly IDispenserService dispenserService;
        private readonly IPaymentService paymentService;
        private readonly RandomCodeGenerator randomCodeGenerator;

        public RentingService(IIotActionServices iotActionServices,
            IRentalSQLService rentalSQLService,
            IDispenserService dispenserService,
            IPaymentService paymentService,
            RandomCodeGenerator randomCodeGenerator)
        {
            this.iotActionServices = iotActionServices;
            this.rentalSQLService = rentalSQLService;
            this.dispenserService = dispenserService;
            this.paymentService = paymentService;
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

            //get dispenser iot name
            var dispenserIotName = await dispenserService.GetDispenserIotNameAsync(rental.DispenserId);

            //send dispenser action
            await iotActionServices.InformDispenserOfActionAsync(dispenserIotName, actions);

            return rentalGuid;
        }

        private Actions CreateActions(Rental rental)
        {
            return new Actions
            {
                ActionType = ActionType.sendcode,
                LockerCode = rental.LockerCode,
                LockerNumber = rental.ItemRentalDetails.LockerNumber
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

            var dispenserIotName = await dispenserService.GetDispenserIotNameAsync(rental.DispenserId);

            await iotActionServices.InformDispenserOfActionAsync(dispenserIotName, BeginRentalAction(rental));
        }

        private Actions BeginRentalAction(Rental rental)
        {
            return new Actions
            {
                ActionType = ActionType.unlock,
                LockerNumber = rental.ItemRentalDetails.LockerNumber
            };
        }

        /// <summary>
        /// look to see rental status
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        public async Task<bool> CheckRentalStatusAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var rental = await rentalSQLService.GetRentalAsync(rentalId);

            return rental.HasBeenReturned;
        }

        /// <summary>
        /// return a rental. Trigger action to open locker
        /// </summary>
        /// <param name="rental">rental object</param>
        /// <returns></returns>
        public async Task ReturnRentalItemAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            var dispenserIotName = await dispenserService.GetDispenserIotNameAsync(rental.DispenserId);

            await iotActionServices.InformDispenserOfActionAsync(dispenserIotName, ReturnRentalAction(rental));
        }

        private Actions ReturnRentalAction(Rental rental)
        {
            return new Actions
            {
                ActionType = ActionType.unlock,
                LockerNumber = rental.ItemRentalDetails.LockerNumber
            };
        }

        /// <summary>
        /// Complete rental
        /// </summary>
        /// <param name="rentalId">pk of rental</param>
        /// <returns></returns>
        public async Task CompleteRentalAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var rental = await rentalSQLService.GetRentalAsync(rentalId);
            var state = await dispenserService.GetDispenserStateAsync(rental.DispenserId);
            rental = await paymentService.CalculateRentalPriceAsync(rental, state);

            await rentalSQLService.CompleteRentalAsync(rental);
        }
    }
}