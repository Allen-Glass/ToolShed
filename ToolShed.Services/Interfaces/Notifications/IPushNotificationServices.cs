using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Notifications;

namespace ToolShed.Services.Interfaces.Notifications
{
    public interface IPushNotificationServices
    {
        Task SendAsync(PushNotification notification, CancellationToken cancellation = default);
    }
}
