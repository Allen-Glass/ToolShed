using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class RentalRepository
    {
        private readonly ToolShedContext toolShedContext;

        public RentalRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddAsync(Rental rental, CancellationToken cancellationToken = default)
        {
            if (rental == null)
                throw new ArgumentNullException();

            await toolShedContext.RentalSet
                .AddAsync(rental, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);

            return rental.RentalId;
        }

        public async Task<Rental> GetAsync(Guid rentalId, CancellationToken cancellationToken = default)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.RentalSet
                .FirstOrDefaultAsync(c => c.RentalId.Equals(rentalId), cancellationToken);
        }

        public async Task<IEnumerable<Rental>> ListAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.RentalSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> CheckLockerCodeAsync(Guid rentalId, string lockerCode, CancellationToken cancellationToken = default)
        {
            if (rentalId == Guid.Empty || lockerCode == string.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.RentalSet
                .Where(c => c.RentalId.Equals(rentalId))
                .AnyAsync(c => c.LockerCode.Equals(lockerCode), cancellationToken);
        }

        public async Task CompleteRentalAsync(Guid rentalId, CancellationToken cancellationToken = default)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var rental = await GetAsync(rentalId);

            if (rental == null)
                throw new ArgumentNullException();

            rental.RentalReturnTime = DateTime.UtcNow;
            rental.HasBeenReturned = true;

            toolShedContext.Update(rental);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task CompleteRentalAsync(Rental rental, CancellationToken cancellationToken = default)
        {
            if (rental == null)
                throw new ArgumentNullException();

            rental.RentalReturnTime = DateTime.UtcNow;
            rental.HasBeenReturned = true;

            toolShedContext.Update(rental);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
