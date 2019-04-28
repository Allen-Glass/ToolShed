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

        public async Task AddCardAddressAsync(CardAddress cardAddress)
        {
            await toolShedContext.CardAddressSet
                .AddAsync(cardAddress);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteCardAddresssAsync(CardAddress cardAddress)
        {
            toolShedContext.CardAddressSet
                .Remove(cardAddress);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
