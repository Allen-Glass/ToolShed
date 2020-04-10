using Newtonsoft.Json;
using System.Collections.Generic;

namespace ToolShed.Models.Notifications
{
    /// <summary>
    /// Push core payload object
    /// </summary>
    public class ApplePushNotification
    {
        public ApplePushNotification(string body, Dictionary<string, string> payload)
        {
            Aps = new Aps(body);
            Payload = payload;
        }

        [JsonProperty(PropertyName = "aps")]
        public Aps Aps { get; set; }

        [JsonProperty(PropertyName = "payload")]
        public Dictionary<string, string> Payload { get; set; }
    }

    /// <summary>
    /// Apple push service request model
    /// </summary>
    public class Aps
    {
        public Aps(string body)
        {
            Alert = body;
            ContentAvailable = 1;
        }

        [JsonProperty(PropertyName = "alert")]
        public string Alert { get; set; }

        [JsonProperty(PropertyName = "content-available")]
        public int ContentAvailable { get; set; }
    }
}
