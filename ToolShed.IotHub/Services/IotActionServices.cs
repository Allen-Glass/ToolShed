using Microsoft.Azure.Devices;
using System;
using System.Text;
using System.Threading.Tasks;
using ToolShed.IotHub.Interfaces;

namespace ToolShed.IotHub.Services
{
    public class IotActionServices : IIotActionServices
    {
        private readonly ServiceClient serviceClient;

        public IotActionServices(ServiceClient serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        public async Task SendMessageToDispenser(string deviceName, string message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            await serviceClient.SendAsync(deviceName, commandMessage);
        }
    }
}
