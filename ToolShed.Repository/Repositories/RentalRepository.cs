using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Guid> AddAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            await toolShedContext.RentalSet
                .AddAsync(rental);
            await toolShedContext.SaveChangesAsync();

            return rental.RentalId;
        }

        public async Task<Rental> GetAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.RentalSet
                .FirstOrDefaultAsync(c => c.RentalId.Equals(rentalId));
        }

        public async Task<IEnumerable<Rental>> ListAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.RentalSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync();
        }

        public async Task<bool> CheckLockerCodeAsync(Guid rentalId, string lockerCode)
        {
            if (rentalId == Guid.Empty || lockerCode == string.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.RentalSet
                .Where(c => c.RentalId.Equals(rentalId))
                .AnyAsync(c => c.LockerCode.Equals(lockerCode));
        }

        public async Task CompleteRentalAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var rental = await GetAsync(rentalId);

            if (rental == null)
                throw new ArgumentNullException();

            rental.RentalReturnTime = DateTime.UtcNow;
            rental.HasBeenReturned = true;

            toolShedContext.Update(rental);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task CompleteRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            rental.RentalReturnTime = DateTime.UtcNow;
            rental.HasBeenReturned = true;

            toolShedContext.Update(rental);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
