using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.IotHub.Interfaces
{
    public interface IIotActionServices
    {
        Task SendMessageToDispenser(string deviceName, string message);

        Task InformDispenserOfActionAsync(string deviceId, Actions action);
    }
}
