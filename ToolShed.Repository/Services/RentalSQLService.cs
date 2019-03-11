using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Repository.Repositories;

namespace ToolShed.Repository.Services
{
    public class RentalSQLService : IRentalSQLService
    {
        private readonly RentalRepository rentalRepository;
        private readonly UserRepository userRepository;

        public RentalSQLService(RentalRepository rentalRepository
            , UserRepository userRepository)
        {
            this.rentalRepository = rentalRepository;
            this.userRepository = userRepository;
        }

        public async Task CreateNewRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            var rentalId = await rentalRepository.AddRentalAsync(CreateDtoRental(rental));
        }

        public async Task CompleteRentalAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            await rentalRepository.MarkRentalAsCompleteAsync(rentalId);
        }

        public async Task ChargeUserFullPriceAsync()
        {

        }

        private Models.Repository.Rental CreateDtoRental(Rental rental)
        {
            return new Models.Repository.Rental
            {
                RentalStart = DateTime.UtcNow,
                RentalDue = rental.RentalDue,
                PricePerHour = rental.PricePerHour,
                HasBeenReturned = false,
                IsUserOwnedNow = false,
                UserId = rental.User.UserId
            };
        }
    }
}
