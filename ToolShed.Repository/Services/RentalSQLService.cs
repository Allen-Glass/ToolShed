using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task CreateNewRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            var rentalId = await rentalRepository.AddRentalAsync(RentalMapping.CreateDtoRental(rental));
            await rentalRecordsRepository.AddRentalRecordAsync(RentalMapping.CreateDtoRentalRecord(rental));
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
