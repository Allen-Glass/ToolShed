using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Notifications;
using ToolShed.Services.Interfaces.Notifications;

namespace ToolShed.Services.Google
{
    public class FirebaseMessagingService : IPushNotificationServices
    {
        private readonly FirebaseMessaging firebaseMessaging;
        private readonly ILogger<FirebaseMessagingService> logger;

        public FirebaseMessagingService(FirebaseMessaging firebaseMessaging,
            ILogger<FirebaseMessagingService> logger)
        {
            this.firebaseMessaging = firebaseMessaging;
            this.logger = logger;
        }

        /// <summary>
        /// Send notification to user that their proofing failed
        /// </summary>
        /// <param name="notification">notification object</param>
        /// <param name="cancellation"></param>
        public async Task SendAsync(PushNotification notification, CancellationToken cancellation = default)
        {
            if (notification.User == null)
                throw new ArgumentNullException(nameof(notification.User));

            if (string.IsNullOrEmpty(notification.DeviceToken))
                throw new ArgumentNullException(nameof(notification.PushNotificationProperties.DeviceToken));

            await SendMessageAsync(notification, cancellation);
        }

        /// <summary>
        /// prepare and send message to firebase servers
        /// </summary>
        /// <param name="notification">user notification</param>
        private async Task SendMessageAsync(PushNotification notification, CancellationToken cancellation = default)
        {
            var message = new Message
            {
                Data = notification.PushNotificationProperties.Payload,
                Token = notification.PushNotificationProperties.DeviceToken
            };

            try
            {
                await firebaseMessaging.SendAsync(message, cancellation);
                logger.LogInformation($"UserId, {notification.User.UserId}, had a notification sent.");
            }
            catch (Exception e)
            {
                logger.LogError($"User, {notification.User.UserId}, failed to send push notification. Exception details: {e.InnerException}.");
                throw new HttpRequestException(
                    $"There was an issue sending a request to firebase push notification services. exception: {e}. Exception Details: {e.InnerException}.");
            }
        }
    }
}
