using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ToolShed
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                if (!context.HostingEnvironment.IsDevelopment())
                {
                    var appConfig = config.Build();
                    var certStore = new X509Store(StoreLocation.CurrentUser);
                    certStore.Open(OpenFlags.ReadOnly |
                                   OpenFlags.OpenExistingOnly);
                    var cert = certStore.Certificates.Find(X509FindType.FindByThumbprint
                        , appConfig["CertificateThumbprint"]
                        , false);
                    config.AddAzureKeyVault(
                        appConfig["KeyVaultUrl"]
                        , appConfig["ClientId"]
                        , cert.OfType<X509Certificate2>().FirstOrDefault());
                }
            })
                .UseStartup<Startup>();
    }
}
