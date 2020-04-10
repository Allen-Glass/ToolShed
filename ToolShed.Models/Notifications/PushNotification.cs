using ToolShed.Models.API;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Notifications
{
    public class PushNotification : Notification
    {
        public PushNotification(User user, PushNotificationProperties pushNotificationProperties)
        {
            NotificationType = NotificationType.PushNotification;
            User = user ?? throw new System.ArgumentNullException(nameof(user));
            Body = pushNotificationProperties.Body;
            PushNotificationProperties = pushNotificationProperties;
        }

        public PushNotificationType PushNotificationType { get; set; }

        public string DeviceToken { get; set; }

        public PushNotificationProperties PushNotificationProperties { get; }
    }
}
