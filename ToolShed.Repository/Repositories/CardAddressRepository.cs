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
    public class CardAddressRepository
    {
        private readonly ToolShedContext toolShedContext;

        public CardAddressRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddAsync(CardAddress cardAddress, CancellationToken cancellationToken = default)
        {
            if (cardAddress == null)
                throw new ArgumentNullException(nameof(cardAddress));

            await toolShedContext.CardAddressSet
                .AddAsync(cardAddress, cancellationToken);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Guid>> GetAsync(Guid cardId, CancellationToken cancellationToken = default)
        {
            if (cardId == Guid.Empty)
                throw new ArgumentNullException(nameof(cardId));

            return await toolShedContext.CardAddressSet
                .Where(c => c.CardId.Equals(cardId))
                .Select(c => c.AddressId)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(CardAddress cardAddress, CancellationToken cancellationToken = default)
        {
            if (cardAddress == null)
                throw new ArgumentNullException(nameof(cardAddress));

            toolShedContext.CardAddressSet
                .Remove(cardAddress);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
