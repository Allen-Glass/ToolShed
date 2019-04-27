using Microsoft.Azure.Management.IotHub;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Services.Interfaces;

namespace ToolShed.IotHub.Services
{
    public class IotManagementService : IIotManagementService
    {
        private readonly IIotHubClient iotHubClient;

        public IotManagementService(IIotHubClient iotHubClient)
        {
            this.iotHubClient = iotHubClient;
        }

        public async Task AddIotDeviceAsync()
        {

        }
    }
}
