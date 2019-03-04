using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToolShed.IotHub.Interfaces
{
    public interface IIotActionServices
    {
        Task SendMessageToDispenser(string deviceName, string message);
    }
}
