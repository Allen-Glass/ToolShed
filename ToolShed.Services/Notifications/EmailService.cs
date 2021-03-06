﻿using System;
using System.Net.Http;
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
            var httpContent = email.PrepareHttpContent();
            var response = await client.PostAsync(EmailConstants.SendJoshEmailBaseURL, httpContent);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(
                    $"Unable to send email to {email}. {await response.Content.ReadAsStringAsync()}");
        }

        public async Task SendUserInviteToTenantAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            var client = httpClientFactory.CreateClient(EmailConstants.InviteBaseUrl);
            var httpContent = email.PrepareHttpContent();
            var response = await client.PostAsync(EmailConstants.InviteClient, httpContent);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(
                    $"Unable to send email to {email}. {await response.Content.ReadAsStringAsync()}");
        }
    }
}
