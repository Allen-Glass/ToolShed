using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces.Notifications;

namespace ToolShed.Services.Interfaces.Factory
{
    public interface IPushNotificationFactory
    {
        IPushNotificationServices CreatePushNotificationServiceInstance(NotificationProvider notificationProvider);
    }
}
