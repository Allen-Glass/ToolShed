using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ToolShed.Models.Constants;
using ToolShed.Services.Interfaces;
using Xunit;

namespace ToolShed.Services.Tests
{
    public class EmailServiceTests
    {
        private readonly IEmailService emailService;

        public EmailServiceTests()
        {
            emailService = CreateEmailService();
        }

        [Fact]
        public async Task SendJoshEmail()
        {
            var email = "gunneroc@gmail.com";

            await emailService.SendPasswordResetEmailAsync(email);
        }

        private IEmailService CreateEmailService()
        {
            var moqClientFactory = new Mock<IHttpClientFactory>();
            var newClient = new HttpClient { BaseAddress = new Uri(EmailConstants.SendJoshEmailBaseURL) };
            newClient.DefaultRequestHeaders.Add("Authorization", "Token");

            moqClientFactory.Setup(c => c.CreateClient(EmailConstants.SendJoshEmailClient))
                .Returns(newClient);
            return new EmailService(moqClientFactory.Object);
        }
    }
}
