using System.Threading.Tasks;
using Toolshed.Models.SQL;
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

        public async Task AddCardAddressAsync(CardAddress cardAddress)
        {
            await toolShedContext.CardAddressesSet
                .AddAsync(cardAddress);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteCardAddresssAsync(CardAddress cardAddress)
        {
            toolShedContext.CardAddressesSet
                .Remove(cardAddress);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
