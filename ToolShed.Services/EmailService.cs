using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ToolShed.Services
{
    public class EmailService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public EmailService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task SendPasswordResetEmail()
        {

        }
    }
}
