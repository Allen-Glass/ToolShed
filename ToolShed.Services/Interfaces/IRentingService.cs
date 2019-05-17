using System;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Services.Interfaces
{
    public interface IRentingService
    {
        /// <summary>
        /// Preorder rental
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        Task<Guid> PlaceRentalAsync(Rental rental);

        /// <summary>
        /// Begin rental
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        Task StartRentalAsync(Rental rental);

        /// <summary>
        /// check the current status of a rental
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        Task<bool> CheckRentalStatusAsync(Guid rentalId);

        /// <summary>
        /// return rental
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        Task ReturnRentalItemAsync(Rental rental);

        /// <summary>
        /// complete user rental
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        Task CompleteRentalAsync(Guid rentalId);
    }
}