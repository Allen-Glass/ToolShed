using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using ToolShed.IotHub.Interfaces;
using ToolShed.Models.API;

namespace ToolShed.Services.Iot
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

        public async Task InformDispenserOfActionAsync(string deviceId, Actions action)
        {
            var serializedAction = JsonConvert.SerializeObject(action);
            var message = new Message(Encoding.ASCII.GetBytes(serializedAction));
            await serviceClient.SendAsync(deviceId, message);
        }
    }
}
