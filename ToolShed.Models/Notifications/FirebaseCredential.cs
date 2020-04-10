using Newtonsoft.Json;

namespace ToolShed.Models.Notifications
{
    /// <summary>
    /// Authenticate to firebase messaging via service account
    /// https://firebase.google.com/docs/cloud-messaging/auth-server
    /// </summary>
    public class FirebaseCredential
    {
        /// <summary>
        /// type of authentication
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// id of app in firebase
        /// </summary>
        [JsonProperty(PropertyName = "project_id")]
        public string ProjectId { get; set; }

        /// <summary>
        /// unique identifier for private key in firebase
        /// </summary>
        [JsonProperty(PropertyName = "private_key_id")]
        public string PrivateKeyId { get; set; }

        /// <summary>
        /// private key
        /// </summary>
        [JsonProperty(PropertyName = "private_key")]
        public string PrivateKey { get; set; }

        /// <summary>
        /// address of service account
        /// </summary>
        [JsonProperty(PropertyName = "client_email")]
        public string ClientEmail { get; set; }

        /// <summary>
        /// id of app in firebase
        /// </summary>
        [JsonProperty(PropertyName = "client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// oauth google api endpoint
        /// </summary>
        [JsonProperty(PropertyName = "auth_uri")]
        public string AuthUri { get; set; }

        /// <summary>
        /// oauth google api token endpoint
        /// </summary>
        [JsonProperty(PropertyName = "token_uri")]
        public string TokenUri { get; set; }

        /// <summary>
        /// service account cert provider url
        /// </summary>
        [JsonProperty(PropertyName = "auth_provider_x509_cert_url")]
        public string AuthProviderX509CertUrl { get; set; }

        /// <summary>
        /// service account cert provider url
        /// </summary>
        [JsonProperty(PropertyName = "client_x509_cert_url")]
        public string ClientX509CertUrl { get; set; }
    }
}