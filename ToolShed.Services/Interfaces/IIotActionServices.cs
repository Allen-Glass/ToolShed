using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.IoTHub;

namespace ToolShed.IotHub.Interfaces
{
    public interface IIotActionServices
    {
        Task SendMessageToDispenser(string deviceName, string message);

        Task InformDispenserOfActionAsync(string deviceId, Actions action);
    }
}
