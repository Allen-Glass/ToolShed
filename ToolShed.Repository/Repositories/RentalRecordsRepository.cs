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
    public class RentalRecordsRepository
    {
        private readonly ToolShedContext toolShedContext;

        public RentalRecordsRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(RentalRecord rentalRecord, CancellationToken cancellationToken = default)
        {
            if (rentalRecord == null)
                throw new ArgumentNullException();

            await toolShedContext.RentalRecordSet
                .AddAsync(rentalRecord, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<RentalRecord>> ListAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            var rentalRecords = await toolShedContext.RentalRecordSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync(cancellationToken);

            if (rentalRecords == null)
                throw new NullReferenceException();

            return rentalRecords;
        }

        public async Task<RentalRecord> GetAsync(Guid rentalRecordId, CancellationToken cancellationToken = default)
        {
            if (rentalRecordId == Guid.Empty)
                throw new ArgumentNullException();

            var rentalRecord = await toolShedContext.RentalRecordSet
                .FirstOrDefaultAsync(c => c.UserId.Equals(rentalRecordId), cancellationToken);

            if (rentalRecord == null)
                throw new NullReferenceException();

            return rentalRecord;
        }
    }
}
