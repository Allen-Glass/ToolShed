using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Toolshed.SignalR.Hubs;
using Toolshed.SignalR.Interfaces;

namespace Toolshed.SignalR.Services
{
    public class DispenserActionService : IDispenserActionService
    {
        private readonly IHubContext<DispenserHub> dispenserHub;

        public DispenserActionService(IHubContext<DispenserHub> dispenserHub)
        {
            this.dispenserHub = dispenserHub;
        }

        public async Task SendActionToDispenser(string message, Guid dispenserId)
        {
            await dispenserHub.Clients.User(dispenserId.ToString()).SendAsync("", message);
        }

        public async Task BroadCastMessage(string message)
        {
            await dispenserHub.Clients.All.SendAsync("Broadcast", message);
        }
    }
}
