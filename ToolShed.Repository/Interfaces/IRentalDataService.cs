using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface IRentalDataService
    {
        /// <summary>
        /// create a new rental
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        Task<Guid> CreateNewRentalAsync(Rental rental, CancellationToken cancellationToken = default);

        /// <summary>
        /// get rental
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        Task<Rental> GetRentalAsync(Guid rentalId, CancellationToken cancellationToken = default);

        /// <summary>
        /// get a rental with user information
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        Task<Rental> GetRentalWithUserInformationAsync(Guid rentalId, CancellationToken cancellationToken = default);

        /// <summary>
        /// get the locker code of a rental
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        Task<string> GetLockerCodeAsync(Guid rentalId, CancellationToken cancellationToken = default);

        /// <summary>
        /// check to see if the locker code is correct
        /// </summary>
        /// <param name="rental"></param>
        /// <returns></returns>
        Task<bool> CheckLockerCodeAsync(Rental rental, CancellationToken cancellationToken = default);

        /// <summary>
        /// Complete rental
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        Task CompleteRentalAsync(Guid rentalId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Complete rental
        /// </summary>
        /// <param name="rental">rental object</param>
        Task CompleteRentalAsync(Rental rental, CancellationToken cancellationToken = default);

        /// <summary>
        /// if user overextends rental duration to the point of paying full price they will be charged full price
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        Task ChargeUserFullPriceAsync(Guid rentalId, CancellationToken cancellationToken = default);
    }
}
