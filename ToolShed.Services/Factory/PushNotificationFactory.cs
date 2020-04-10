using System;
using System.Collections.Generic;
using System.Text;
using ToolShed.Models.Enums;
using ToolShed.Services.Interfaces.Factory;
using ToolShed.Services.Interfaces.Notifications;

namespace ToolShed.Services.Factory
{
    public class PushNotificationFactory : IPushNotificationFactory
    {
        private readonly Dictionary<NotificationProvider, IPushNotificationServices> notificationServices;

        public PushNotificationFactory(Dictionary<NotificationProvider, IPushNotificationServices> notificationServices)
        {
            this.notificationServices = notificationServices;
        }

        /// <summary>
        /// create an instance of INotificationService
        /// </summary>
        /// <param name="notificationType">how user will receive notification</param>
        /// <returns>notification service instance</returns>
        public IPushNotificationServices CreatePushNotificationServiceInstance(NotificationProvider notificationProvider)
        {
            if (notificationServices.TryGetValue(notificationProvider, out var notificationServiceInstance))
                return notificationServiceInstance;

            throw new KeyNotFoundException(nameof(notificationServiceInstance));
        }
    }
}
