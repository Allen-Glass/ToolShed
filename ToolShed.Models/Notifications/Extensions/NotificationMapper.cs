using System;

namespace ToolShed.Models.Notifications.Extensions
{
    public static class NotificationMapper
    {
        public static ApplePushNotification CreateApplePushNotification(this PushNotification pushNotification)
        {
            if (pushNotification == null)
                throw new ArgumentNullException(nameof(pushNotification));

            return new ApplePushNotification(pushNotification.Body, pushNotification.PushNotificationProperties.Payload);
        }
    }
}
