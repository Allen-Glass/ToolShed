using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddAsync(CardAddress cardAddress)
        {
            await toolShedContext.CardAddressSet
                .AddAsync(cardAddress);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guid>> GetAsync(Guid cardId)
        {
            if (cardId == Guid.Empty)
                throw new ArgumentNullException();

            return await toolShedContext.CardAddressSet
                .Where(c => c.CardId.Equals(cardId))
                .Select(c => c.AddressId)
                .ToListAsync();
        }

        public async Task DeleteAsync(CardAddress cardAddress)
        {
            toolShedContext.CardAddressSet
                .Remove(cardAddress);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
