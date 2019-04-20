using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Helpers;
using ToolShed.Models.Constants;
using ToolShed.Services.Interfaces;

namespace ToolShed.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public EmailService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task SendPasswordResetEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            var client = httpClientFactory.CreateClient(EmailConstants.SendJoshEmailClient);
            var httpContent = RequestExtensions.PrepareHttpContent(email);
            var response = await client.PostAsync(EmailConstants.SendJoshEmailBaseURL, httpContent);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(
                    $"Unable to send email to {email}. {await response.Content.ReadAsStringAsync()}");
        }
    }
}
