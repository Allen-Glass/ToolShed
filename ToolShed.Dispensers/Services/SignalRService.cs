using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.SignalR.Hubs;

namespace ToolShed.Dispensers.Services
{
    public class SignalRService
    {
        private readonly IHubContext<DispenserHub> dispenserHub;

        public SignalRService(IHubContext<DispenserHub> dispenserHub)
        {
            this.dispenserHub = dispenserHub;
        }

        public async Task RegisterToHub()
        {

        }
    }
}
