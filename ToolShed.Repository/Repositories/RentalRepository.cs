using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Guid> AddRentalAsync(Rental rental)
        {
            if (rental == null)
                throw new ArgumentNullException();

            await toolShedContext.RentalSet
                .AddAsync(rental);
            await toolShedContext.SaveChangesAsync();

            return rental.RentalId;
        }

        public async Task<Rental> GetRentalByRentalIdAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.RentalSet
                .FirstOrDefaultAsync(c => c.RentalId.Equals(rentalId));
        }

        public async Task<IEnumerable<Rental>> GetRentalsByUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.RentalSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync();
        }

        public async Task MarkRentalAsCompleteAsync(Guid rentalId)
        {
            if (rentalId == Guid.Empty)
                throw new ArgumentNullException();

            var rental = await GetRentalByRentalIdAsync(rentalId);

            if (rental == null)
                throw new ArgumentNullException();

            rental.RentalReturned = DateTime.UtcNow;
            rental.HasBeenReturned = true;

            toolShedContext.Update(rental);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
