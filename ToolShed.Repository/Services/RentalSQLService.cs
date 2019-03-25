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

        public RentalSQLService(RentalRepository rentalRepository
            , UserRepository userRepository
            , RentalRecordsRepository rentalRecordsRepository)
        {
            this.rentalRepository = rentalRepository;
            this.userRepository = userRepository;
            this.rentalRecordsRepository = rentalRecordsRepository;
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

            if (dtoRental == null)
                throw new NullReferenceException();

            return RentalMapping.ConvertDtoRentalToRental(dtoRental);
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

            await rentalRepository.MarkRentalAsCompleteAsync(rentalId);
            var dtoRental = await rentalRepository.GetRentalByRentalIdAsync(rentalId);
            await rentalRecordsRepository.AddRentalRecordAsync(RentalMapping.CreateCompleteDtoRentalRecord(dtoRental));
        }

        public async Task ChargeUserFullPriceAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();


        }
    }
}
