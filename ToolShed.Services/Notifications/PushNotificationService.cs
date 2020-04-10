using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Enums;
using ToolShed.Models.Notifications;
using ToolShed.Services.Interfaces.Factory;
using ToolShed.Services.Interfaces.Notifications;

namespace ToolShed.Services.Notifications
{
    public class PushNotificationService : IPushNotificationServices
    {
        private readonly IPushNotificationFactory notificationFactory;
        private readonly ILogger<PushNotificationService> logger;

        public PushNotificationService(IPushNotificationFactory notificationFactory,
            ILogger<PushNotificationService> logger)
        {
            this.notificationFactory = notificationFactory;
            this.logger = logger;
        }

        /// <summary>
        /// Send user notification
        /// </summary>
        /// <param name="notification">notification object</param>
        /// <param name="cancellation"></param>
        public async Task SendAsync(PushNotification notification, CancellationToken cancellation = default)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            try
            {
                var notificationService = notificationFactory.CreatePushNotificationServiceInstance(DetermineNotificationProvider(notification));
                await notificationService.SendAsync(notification, cancellation);
            }
            catch (Exception e)
            {
                logger.LogError($"UserId, {notification.User.UserId}, failed to be notified. Exception details: {e.InnerException}");
                throw new Exception($"UserId, {notification.User.UserId}, failed to be notified. Exception details: {e}"); //make unique exception...
            }
        }

        /// <summary>
        /// Determine how to inform user of notification
        /// </summary>
        /// <param name="notification">notification object</param>
        /// <returns>the notification provider</returns>
        private NotificationProvider DetermineNotificationProvider(PushNotification notification)
        {
            if (notification.PushNotificationProperties.DeviceType == DeviceType.Android)
                return NotificationProvider.Google;

            if (notification.PushNotificationProperties.DeviceType == DeviceType.iOS)
                return NotificationProvider.Apple;

            return NotificationProvider.SignalR; //no azure push notification hub right now
        }
    }
}
