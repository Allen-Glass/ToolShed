using System;
using System.Threading;
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
            this.rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.rentalRecordsRepository = rentalRecordsRepository ?? throw new ArgumentNullException(nameof(rentalRecordsRepository));
            this.dispenserRepository = dispenserRepository ?? throw new ArgumentNullException(nameof(dispenserRepository));
            this.itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            this.itemRentalDetailsRepository = itemRentalDetailsRepository ?? throw new ArgumentNullException(nameof(itemRentalDetailsRepository));
        }

        public async Task<Guid> CreateNewRentalAsync(Rental rental, CancellationToken cancellationToken = default)
        {
            if (rental == null)
                throw new ArgumentNullException(nameof(rental));

            var rentalId = await rentalRepository.AddAsync(rental.CreateDtoRental(), cancellationToken);
            await rentalRecordsRepository.AddAsync(rental.CreateDtoRentalRecord(), cancellationToken);

            return rentalId;
        }

        public async Task<Rental> GetRentalAsync(Guid rentalId, CancellationToken cancellationToken = default)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException(nameof(rentalId));

            var dtoRental = await rentalRepository.GetAsync(rentalId, cancellationToken);
            var dtoItemRentalDetails = await itemRentalDetailsRepository.GetAsync(dtoRental.ItemRentalDetailsId, cancellationToken);
            var dtoItem = itemRepository.GetAsync(dtoItemRentalDetails.ItemId, cancellationToken);
            var dtoUser = userRepository.GetAsync(dtoRental.UserId, cancellationToken);

            if (dtoRental == null)
                throw new NullReferenceException(nameof(dtoRental));

            var rental = dtoRental.ConvertDtoRentalToRental();
            var user = UserMapping.ConvertDtoUser(await dtoUser);
            var itemRentalDetails = ItemMapping.ConvertItemRentalDetails(dtoItemRentalDetails, await dtoItem);
            rental.User = user;
            rental.ItemRentalDetails = itemRentalDetails;

            return rental;
        }

        public async Task<Rental> GetRentalWithUserInformationAsync(Guid rentalId, CancellationToken cancellationToken = default)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException(nameof(rentalId));

            var dtoRental = await rentalRepository.GetAsync(rentalId, cancellationToken);

            if (dtoRental == null)
                throw new NullReferenceException();

            var rental = dtoRental.ConvertDtoRentalToRental();
            var dtoUser = await userRepository.GetAsync(dtoRental.UserId, cancellationToken);
            rental.User = dtoUser.ConvertDtoUser();

            return rental;
        }

        public async Task<string> GetLockerCodeAsync(Guid rentalId, CancellationToken cancellationToken = default)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException(nameof(rentalId));

            var dtoRental = await rentalRepository.GetAsync(rentalId, cancellationToken);

            return dtoRental.LockerCode;
        }

        public async Task<bool> CheckLockerCodeAsync(Rental rental, CancellationToken cancellationToken = default)
        {
            if (rental.LockerCode == string.Empty || rental.RentalId == Guid.Empty)
                throw new ArgumentNullException(nameof(rental));

            return await rentalRepository.CheckLockerCodeAsync(rental.RentalId, rental.LockerCode, cancellationToken);
        }

        public async Task CompleteRentalAsync(Guid rentalId, CancellationToken cancellationToken = default)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException(nameof(rentalId));

            await rentalRepository.CompleteRentalAsync(rentalId);
            var dtoRental = await rentalRepository.GetAsync(rentalId);
            await rentalRecordsRepository.AddAsync(dtoRental.CreateCompleteDtoRentalRecord(), cancellationToken);
        }

        public async Task CompleteRentalAsync(Rental rental, CancellationToken cancellationToken = default)
        {
            if (rental == null)
                throw new ArgumentNullException(nameof(rental));

            var dtoRental = RentalMapping.CreateDtoRental(rental);
            await rentalRepository.CompleteRentalAsync(dtoRental);
            await rentalRecordsRepository.AddAsync(dtoRental.CreateCompleteDtoRentalRecord(), cancellationToken);
        }

        public async Task ChargeUserFullPriceAsync(Guid rentalId, CancellationToken cancellationToken = default)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException(nameof(rentalId));

            
        }
    }
}
