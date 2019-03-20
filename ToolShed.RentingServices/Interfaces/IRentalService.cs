using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.RentingServices.Interfaces
{
    public interface IRentalService
    {
        Task PlaceRentalAsync(Rental rental);

        Task StartRentalAsync(Guid rentalId);
    }
}
