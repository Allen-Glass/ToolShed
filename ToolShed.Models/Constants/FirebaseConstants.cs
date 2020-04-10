namespace ToolShed.Models.Constants
{
    public class FirebaseConstants
    {
        /// <summary>
        /// type of authentication
        /// </summary>
        public const string Type = "service_account";

        /// <summary>
        /// auth endpoint
        /// </summary>
        public const string AuthUri = "https://accounts.google.com/o/oauth2/auth";

        /// <summary>
        /// token uri
        /// </summary>
        public const string TokenUri = "https://oauth2.googleapis.com/token";

        /// <summary>
        /// provider of auth cert
        /// </summary>
        public const string AuthProviderX509CertUrl = "https://www.googleapis.com/oauth2/v1/certs";

        /// <summary>
        /// url for firebase push notification server
        /// </summary>
        public const string FirebaseUrl = "https://www.googleapis.com/auth/firebase.messaging";
    }
}
