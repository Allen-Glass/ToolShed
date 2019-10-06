using System;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Mapping;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class RentalDataService : IRentalDataService
    {
        private readonly RentalRepository rentalRepository;
        private readonly RentalRecordsRepository rentalRecordsRepository;
        private readonly UserRepository userRepository;
        private readonly DispenserRepository dispenserRepository;
        private readonly ItemRepository itemRepository;
        private readonly ItemRentalDetailsRepository itemRentalDetailsRepository;

        public RentalDataService(RentalRepository rentalRepository,
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

            var rentalId = await rentalRepository.AddAsync(RentalMapping.CreateDtoRental(rental));
            rentalRecordsRepository.AddAsync(RentalMapping.CreateDtoRentalRecord(rental));

            return rentalId;
        }

        public async Task<Rental> GetRentalAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoRental = await rentalRepository.GetAsync(rentalId);
            var dtoItemRentalDetails = await itemRentalDetailsRepository.GetAsync(dtoRental.ItemRentalDetailsId);
            var dtoItem = itemRepository.GetAsync(dtoItemRentalDetails.ItemId);
            var dtoUser = userRepository.GetAsync(dtoRental.UserId);

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

            var dtoRental = await rentalRepository.GetAsync(rentalId);

            if (dtoRental == null)
                throw new NullReferenceException();

            var rental = RentalMapping.ConvertDtoRentalToRental(dtoRental);
            var dtoUser = await userRepository.GetAsync(dtoRental.UserId);
            rental.User = UserMapping.ConvertDtoUser(dtoUser);

            return rental;
        }

        public async Task<string> GetLockerCodeAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var dtoRental = await rentalRepository.GetAsync(rentalId);

            return dtoRental.LockerCode;
        }

        public async Task<bool> CheckLockerCodeAsync(Rental rental)
        {
            if (rental.LockerCode == string.Empty || rental.RentalId == Guid.Empty)
                throw new ArgumentNullException();

            return await rentalRepository.CheckLockerCodeAsync(rental.RentalId, rental.LockerCode);
        }

        public async Task CompleteRentalAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            await rentalRepository.CompleteRentalAsync(rentalId);
            var dtoRental = await rentalRepository.GetAsync(rentalId);
            await rentalRecordsRepository.AddAsync(RentalMapping.CreateCompleteDtoRentalRecord(dtoRental));
        }

        public async Task CompleteRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            var dtoRental = RentalMapping.CreateDtoRental(rental);
            await rentalRepository.CompleteRentalAsync(dtoRental);
            await rentalRecordsRepository.AddAsync(RentalMapping.CreateCompleteDtoRentalRecord(dtoRental));
        }

        public async Task ChargeUserFullPriceAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            
        }
    }
}
