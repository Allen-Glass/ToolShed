using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface IRentalSQLService
    {
        Task<Guid> CreateNewRentalAsync(Rental rental);
        Task<Rental> GetRentalAsync(Guid rentalId);
        Task<Rental> GetRentalWithUserInformationAsync(Guid rentalId);

        Task<string> GetLockerCodeAsync(Guid rentalId);

            /// <summary>
            /// check to see if the locker code is correct
            /// </summary>
            /// <param name="rental"></param>
            /// <returns></returns>
            Task<bool> CheckLockerCodeAsync(Rental rental);
        Task CompleteRentalAsync(Guid rentalId);
        Task ChargeUserFullPriceAsync(Guid rentalId);
    }
}
