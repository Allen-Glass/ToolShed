using System;
using System.Threading;
using System.Threading.Tasks;
using Toolshed.Models.Scheduler;
using ToolsShed.Scheduler.Interfaces;

namespace ToolsShed.Scheduler
{
    public class Booking : IBooking
    {
        public async Task ReserveTool(Reservation reservation, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CancelReservation(Reservation reservation, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
