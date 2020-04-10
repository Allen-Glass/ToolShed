using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.Notifications;
using ToolShed.Models.Notifications.Extensions;
using ToolShed.Services.Interfaces.Notifications;

namespace ToolShed.Services.Apple
{
    public class APNServices : IPushNotificationServices
    {
        private readonly ApnsServiceBroker apnsServiceBroker;
        private readonly ILogger<APNServices> logger;

        public APNServices(ApnsServiceBroker apnsServiceBroker,
            ILogger<APNServices> logger)
        {
            this.apnsServiceBroker = apnsServiceBroker;
            this.logger = logger;
        }

        /// <summary>
        /// Send tlp push notification to user
        /// </summary>
        /// <param name="notification">notification object</param>
        /// <param name="cancellation"></param>
        public async Task SendAsync(PushNotification notification, CancellationToken cancellationToken = default)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            if (string.IsNullOrEmpty(notification.Body))
                throw new ArgumentNullException(nameof(notification.Body));

            await Task.Run(() => SendApnPushNotification(notification), cancellationToken);
        }

        /// <summary>
        /// Send push notification to APN servers
        /// </summary>
        /// <param name="notification"></param>
        private void SendApnPushNotification(PushNotification notification)
        {
            if (string.IsNullOrEmpty(notification.DeviceToken))
                throw new ArgumentNullException(nameof(notification.DeviceToken));

            if (string.IsNullOrEmpty(notification.Body))
                throw new ArgumentNullException(nameof(notification.Body));

            apnsServiceBroker.OnNotificationFailed += (pushNotification, aggregateEx) =>
            {
                if (notification == null)
                    throw new ArgumentNullException(nameof(pushNotification));

                if (aggregateEx == null)
                    throw new ArgumentNullException(nameof(aggregateEx));

                aggregateEx.Handle(ex => {

                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException notificationException)
                    {
                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        logger.LogError(ex, $"UserId, {notification.User.UserId}, failed to send push notification." +
                                    $"Apple Notification Failed: ID={apnsNotification.Identifier}. Exception Details: Code={statusCode}.");
                        throw new HttpRequestException(
                            $"There was an issue sending a request to apple push notification services. ex: ID={apnsNotification.Identifier}. Exception Details: Code={statusCode} {ex.InnerException}");
                    }

                    // Inner exception might hold more useful information like an ApnsConnectionException			
                    logger.LogError(ex, $"UserId, {notification.User.UserId}, failed to send push notification. Exception details: {ex.InnerException}");
                    throw new HttpRequestException(
                        $"There was an issue sending a request to apple push notification services. Exception Details: {ex.InnerException}");
                });
            };

            apnsServiceBroker.OnNotificationSucceeded += pushNotification =>
            {
                if (pushNotification == null)
                    throw new ArgumentNullException(nameof(pushNotification));

                logger.LogInformation($"UserId, {notification.User.UserId}, had a notification sent");
            };
            apnsServiceBroker.Start();
            apnsServiceBroker.QueueNotification(new ApnsNotification
            {
                DeviceToken = notification.DeviceToken,
                Payload = CreateNotificationPayload(notification)
            });
            apnsServiceBroker.Stop();
        }

        /// <summary>
        /// Prepare jobject payload for push notification
        /// </summary>
        /// <param name="notification">notification object</param>
        /// <returns>json object for payload</returns>
        private JObject CreateNotificationPayload(PushNotification notification) => JObject.FromObject(notification.CreateApplePushNotification());
    }
}
