using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface IRentalSQLService
    {
        Task CreateNewRentalAsync(Rental rental);
        Task CompleteRentalAsync(Guid rentalId);
        Task ChargeUserFullPriceAsync(Guid rentalId);
    }
}
