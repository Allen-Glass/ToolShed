using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Mobile.Extensions;
using ToolShed.Mobile.Models;

namespace ToolShed.Mobile.Services
{
    public class RentalService
    {
        private readonly HttpClient httpClient;

        public RentalService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task StartRentalAsync(Rental rental, CancellationToken cancellationToken)
        {
            var httpContent = RequestExtensions.PrepareHttpContent(rental);

            var response = await httpClient.PostAsync("", httpContent, cancellationToken);
        }

        public async Task ReturnRentalAsync(Rental rental, CancellationToken cancellationToken)
        {
            var httpContent = RequestExtensions.PrepareHttpContent(rental);
            var response = await httpClient.PostAsync("", httpContent, cancellationToken);
        }
    }
}
