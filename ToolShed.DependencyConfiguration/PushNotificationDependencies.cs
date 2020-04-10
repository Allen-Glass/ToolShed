using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ToolShed.Models.Constants;
using ToolShed.Models.Enums;
using ToolShed.Models.Notifications;
using ToolShed.Services.Apple;
using ToolShed.Services.Factory;
using ToolShed.Services.Google;
using ToolShed.Services.Interfaces.Factory;
using ToolShed.Services.Interfaces.Notifications;
using ToolShed.Services.Notifications;

namespace ToolShed.DependencyConfiguration
{
    public static class PushNotificationDependencies
    {
        public static void AddPushNotificationDependencies(this IServiceCollection services,
            IConfiguration configuration)
        {
            AddNotificationService(services);
            AddFirebaseMessagingServices(services, configuration);
            AddApplePushNotificationServices(services, configuration["AppleCertificateThumbprint"]);
        }

        private static void AddNotificationService(IServiceCollection services)
        {
            services.AddTransient<PushNotificationService>();
            services.AddTransient<IPushNotificationServices>(c => c.GetRequiredService<PushNotificationService>());
            services.AddTransient<IPushNotificationFactory, PushNotificationFactory>(sp =>
            {
                var references = new Dictionary<NotificationProvider, IPushNotificationServices>
                {
                    {NotificationProvider.Apple, sp.GetRequiredService<APNServices>()},
                    {NotificationProvider.Google, sp.GetRequiredService<FirebaseMessagingService>()}
                };

                return new PushNotificationFactory(references);
            });
        }

        private static void AddFirebaseMessagingServices(IServiceCollection services,
            KeyVaultClient keyVaultClient)
        {
            var firebasePrivateKey = keyVaultClient.GetSecretAsync(Environment.GetEnvironmentVariable("FirebasePrivateKey")).Result;
            AddFirebaseMessagingServices(services, firebasePrivateKey.Value); //need to acquire env variables to get full firebase credential model
        }

        private static void AddApplePushNotificationServices(IServiceCollection services, string certificateThumbprint)
        {
            var certStore = new X509Store(StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly |
                           OpenFlags.OpenExistingOnly);

            services.AddTransient(sp =>
            {
                var appleCerts = certStore.Certificates.Find(X509FindType.FindByThumbprint,
                certificateThumbprint,
                false);
                var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production,
                    appleCerts.OfType<X509Certificate2>().FirstOrDefault()) ?? throw new ArgumentNullException(nameof(X509Certificate2));
                var apnsBroker = new ApnsServiceBroker(config);
                return new APNServices(apnsBroker, sp.GetRequiredService<ILogger<APNServices>>());
            });
        }

        private static void AddFirebaseMessagingServices(IServiceCollection services,
            IConfiguration configuration)
        {
            var firebaseCredential = new FirebaseCredential
            {
                PrivateKey = configuration["FirebasePrivateKey"],
                Type = FirebaseConstants.Type,
                ProjectId = configuration["ProjectId"],
                PrivateKeyId = configuration["PrivateKeyId"],
                ClientEmail = configuration["ClientEmail"],
                ClientId = configuration["ClientId"],
                AuthUri = FirebaseConstants.AuthUri,
                TokenUri = FirebaseConstants.TokenUri,
                AuthProviderX509CertUrl = FirebaseConstants.AuthProviderX509CertUrl,
                ClientX509CertUrl = configuration["ClientX509CertUrl"]
            };
            var credential = JsonConvert.SerializeObject(firebaseCredential);
            AddFirebaseMessagingServices(services, credential); 
        }

        private static void AddFirebaseMessagingServices(IServiceCollection services,
            string credential)
        {
            var firebaseApp = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(credential).CreateScoped(FirebaseConstants.FirebaseUrl)
            });
            services.AddTransient(sp =>
            {
                var firebaseMessaging = FirebaseMessaging.GetMessaging(firebaseApp);
                return new FirebaseMessagingService(firebaseMessaging,
                    sp.GetRequiredService<ILogger<FirebaseMessagingService>>());
            });
        }
    }
}
