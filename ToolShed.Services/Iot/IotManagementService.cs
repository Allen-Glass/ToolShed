using Microsoft.Azure.Management.IotHub;
using System.Threading.Tasks;
using ToolShed.Services.Interfaces;

namespace ToolShed.Services.Iot
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
