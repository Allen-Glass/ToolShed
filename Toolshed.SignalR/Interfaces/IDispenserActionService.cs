using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Toolshed.SignalR.Interfaces
{
    /// <summary>
    /// Send actions to dispensers
    /// </summary>
    public interface IDispenserActionService
    {
        /// <summary>
        /// Send action to dispenser object
        /// </summary>
        /// <param name="message">the json object containing the action to commit</param>
        /// <param name="dispenserId">pk of dispenser</param>
        Task SendActionToDispenser(string message, Guid dispenserId);
    }
}
