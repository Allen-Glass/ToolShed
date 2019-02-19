using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Toolshed.Models.Scheduler;

namespace ToolsShed.Scheduler.Interfaces
{
    public interface IBooking
    {
        Task ReserveTool(Reservation reservation, CancellationToken cancellationToken);
        Task CancelReservation(Reservation reservation, CancellationToken cancellationToken);
    }
}
