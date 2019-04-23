using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Mobile.Models;

namespace ToolShed.Mobile.Interfaces
{
    public interface IRentalServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rental"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StartRentalAsync(Rental rental, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rental"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ReturnRentalAsync(Rental rental, CancellationToken cancellationToken);
    }
}
