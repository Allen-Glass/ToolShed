using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ToolShed.Models.Enums;
using ToolShed.Services.Factory;
using ToolShed.Services.Interfaces.Queues;
using ToolShed.Services.Interfaces.Factory;
using ToolShed.Services.Queues;

namespace ToolShed.DependencyConfiguration
{
    public static class QueueDependencies
    {
        public static void AddQueueDependencies(this IServiceCollection services, ServiceAuthType serviceAuthType, IConfiguration configuration = null)
        {
            var queueReferences = new Dictionary<QueueType, string>
            { 
                { QueueType.foo, "foo" },
                { QueueType.bar, "bar" }
            };
            services.AddTransient(sp => GetQueueCredential(serviceAuthType, configuration));
            services.AddTransient<ICloudQueue, QueueClientWrapper>();
            services.AddTransient<IQueueFactory, QueueFactory>(sp => new QueueFactory(queueReferences, sp.GetRequiredService<ICloudQueue>(), "<INSERT BASE QUEUE URL FROM CONFIG OR CONST>"));
            services.AddTransient<IQueueService, QueueService>();
        }

        private static TokenCredential GetQueueCredential(ServiceAuthType serviceAuthType, IConfiguration configuration = null)
        {
            switch (serviceAuthType)
            {
                case ServiceAuthType.MSI:
                    return new DefaultAzureCredential();
                case ServiceAuthType.Cert:
                    var certificateStore = new X509Store(StoreLocation.CurrentUser);
                    var certificateThumbprint = configuration["CertificateThumbprint"];
                    certificateStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    var certificateCollection = certificateStore.Certificates.Find(X509FindType.FindByThumbprint,
                        certificateThumbprint,
                        false);
                    var certificate = certificateCollection.OfType<X509Certificate2>().FirstOrDefault()
                        ?? throw new ArgumentNullException($"Certificate with thumbprint, {certificateThumbprint}, could not be found.");
                    return new ClientCertificateCredential(configuration["tenantId"], configuration["clientId"], certificate);
                default:
                    return new ClientSecretCredential(configuration["tenantId"], configuration["clientId"], configuration["clientSecret"]);
            }
        }
    }
}
