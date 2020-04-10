using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToolShed.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string email);

        Task SendUserInviteToTenantAsync(string email);
    }
}
