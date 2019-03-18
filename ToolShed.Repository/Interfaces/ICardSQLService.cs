using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    /// <summary>
    /// Credit card information in sql
    /// </summary>
    public interface ICardSQLService
    {
        /// <summary>
        /// store credit card
        /// </summary>
        /// <param name="card">credit card info</param>
        Task StoreCardInformationAsync(Card card);
    }
}
