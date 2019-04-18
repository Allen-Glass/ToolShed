using System;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class RentalSQLService : IRentalSQLService
    {
        private readonly RentalRepository rentalRepository;
        private readonly RentalRecordsRepository rentalRecordsRepository;
        private readonly UserRepository userRepository;
        private readonly DispenserRepository dispenserRepository;
        private readonly ItemRepository itemRepository;
        private readonly ItemRentalDetailsRepository itemRentalDetailsRepository;

        public RentalSQLService(RentalRepository rentalRepository,
            UserRepository userRepository,
            RentalRecordsRepository rentalRecordsRepository,
            DispenserRepository dispenserRepository,
            ItemRepository itemRepository,
            ItemRentalDetailsRepository itemRentalDetailsRepository)
        {
            this.rentalRepository = rentalRepository;
            this.userRepository = userRepository;
            this.rentalRecordsRepository = rentalRecordsRepository;
            this.dispenserRepository = dispenserRepository;
            this.itemRepository = itemRepository;
            this.itemRentalDetailsRepository = itemRentalDetailsRepository;
        }

        public async Task<Guid> CreateNewRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            var rentalId = await rentalRepository.AddRentalAsync(RentalMapping.CreateDtoRental(rental));
            await rentalRecordsRepository.AddRentalRecordAsync(RentalMapping.CreateDtoRentalRecord(rental));

            return rentalId;
        }

        public async Task<Rental> GetRentalAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoRental = await rentalRepository.GetRentalByRentalIdAsync(rentalId);
            var dtoItemRentalDetails = await itemRentalDetailsRepository.GetItemRentalDetailsAsync(dtoRental.ItemRentalDetailsId);
            var dtoItem = itemRepository.GetItemByItemIdAsync(dtoItemRentalDetails.ItemId);
            var dtoUser = userRepository.GetUserByUserIdAsync(dtoRental.UserId);

            if (dtoRental == null)
                throw new NullReferenceException();

            var rental = RentalMapping.ConvertDtoRentalToRental(dtoRental);
            var user = UserMapping.ConvertDtoUser(await dtoUser);
            var itemRentalDetails = ItemMapping.ConvertItemRentalDetails(dtoItemRentalDetails, await dtoItem);
            rental.User = user;
            rental.ItemRentalDetails = itemRentalDetails;

            return rental;
        }

        public async Task<Rental> GetRentalWithUserInformationAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoRental = await rentalRepository.GetRentalByRentalIdAsync(rentalId);

            if (dtoRental == null)
                throw new NullReferenceException();

            var rental = RentalMapping.ConvertDtoRentalToRental(dtoRental);
            var dtoUser = await userRepository.GetUserByUserIdAsync(dtoRental.UserId);
            rental.User = UserMapping.ConvertDtoUser(dtoUser);

            return rental;
        }

        public async Task<string> GetLockerCodeAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoRental = await rentalRepository.GetRentalByRentalIdAsync(rentalId);

            return dtoRental.LockerCode;
        }

        public async Task<bool> CheckLockerCodeAsync(Rental rental)
        {
            if (rental.LockerCode == string.Empty || rental.RentalId == Guid.Empty)
                throw new ArgumentNullException();

            var workingLockerCode = await rentalRepository.CheckLockerCodeAsync(rental.RentalId, rental.LockerCode);

            if (workingLockerCode == true)
                await rentalRecordsRepository.AddRentalRecordAsync(RentalMapping.CreateSuccessfulLockerCodeRecord(rental));
            else
                await rentalRecordsRepository.AddRentalRecordAsync(RentalMapping.CreateFailingLockerCodeRecord(rental));

            return workingLockerCode;
        }

        public async Task CompleteRentalAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            await rentalRepository.CompleteRentalAsync(rentalId);
            var dtoRental = await rentalRepository.GetRentalByRentalIdAsync(rentalId);
            await rentalRecordsRepository.AddRentalRecordAsync(RentalMapping.CreateCompleteDtoRentalRecord(dtoRental));
        }

        public async Task CompleteRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            var dtoRental = RentalMapping.CreateDtoRental(rental);
            await rentalRepository.CompleteRentalAsync(dtoRental);
            await rentalRecordsRepository.AddRentalRecordAsync(RentalMapping.CreateCompleteDtoRentalRecord(dtoRental));
        }

        public async Task ChargeUserFullPriceAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            
        }
    }
}
