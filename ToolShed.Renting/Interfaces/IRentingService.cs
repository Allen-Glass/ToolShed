﻿using System;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Renting.Interfaces
{
    public interface IRentingservice
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
        Task<Rental> CheckRentalStatusAsync(Guid rentalId);

        /// <summary>
        /// complete user rental
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        Task CompleteRentalAsync(Guid rentalId);
    }
}