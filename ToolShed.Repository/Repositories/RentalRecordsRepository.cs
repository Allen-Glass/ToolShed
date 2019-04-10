using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddRentalRecordAsync(RentalRecord rentalRecord)
        {
            if (rentalRecord == null)
                throw new ArgumentNullException();

            await toolShedContext.RentalRecordSet
                .AddAsync(rentalRecord);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RentalRecord>> GetAllRentalRecordsByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException();

            var rentalRecords = await toolShedContext.RentalRecordSet
                .Where(c => c.UserId.Equals(userId))
                .ToListAsync();

            if (rentalRecords == null)
                throw new NullReferenceException();

            return rentalRecords;
        }

        public async Task<RentalRecord> GetRecordByRecordIdAsync(Guid rentalRecordId)
        {
            if (rentalRecordId == Guid.Empty)
                throw new ArgumentNullException();

            var rentalRecord = await toolShedContext.RentalRecordSet
                .FirstOrDefaultAsync(c => c.UserId.Equals(rentalRecordId));

            if (rentalRecord == null)
                throw new NullReferenceException();

            return rentalRecord;
        }
    }
}
