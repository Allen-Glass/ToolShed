using System;
using System.Collections.Generic;
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
        Task AddCardAsync(Card card);

        /// <summary>
        /// get user cards
        /// </summary>
        /// <param name="userId">user pk</param>
        /// <returns>list of cards</returns>
        Task<IEnumerable<Card>> GetCardsAsync(Guid userId);

        /// <summary>
        /// delete credit cards
        /// </summary>
        /// <param name="cards">credit cards</param>
        /// <returns></returns>
        Task DeleteCardsAsync(IEnumerable<Card> cards);
    }
}
