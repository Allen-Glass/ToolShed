using System;
using System.Collections.Generic;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Notifications
{
    public class PushNotificationProperties
    {
        public PushNotificationProperties(DeviceType deviceType, PushNotificationType pushNotificationType, string body = "")
        {
            DeviceType = deviceType;
            PushNotificationType = pushNotificationType;
            Body = body;
        }

        public DeviceType DeviceType;

        public PushNotificationType PushNotificationType { get; }

        public string Body { get; }

        public int RetryCount { get; set; }

        public string DeviceToken { get; set; }

        public Dictionary<string, string> Payload { get; set; }

        public void AssignRetryCount(int retryValue = 3)
        {
            RetryCount = retryValue;
        }

        public void AssignDevice(string deviceToken)
        {
            if (string.IsNullOrEmpty(deviceToken))
                throw new ArgumentNullException(deviceToken);

            DeviceToken = deviceToken;
        }

        public void AppendPayload(Dictionary<string, string> payload)
        {
            Payload = payload;
        }
    }
}
