using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Services.Interfaces
{
    public interface ILoginService
    {
        Task CreateNewAccountAsync(User user);

        Task<User> GetUserInformationAsync(Guid userId);

        Task SendPasswordResetEmailAsync(string email);

        Task UpdateUserPasswordAsync(Guid userId, string newPassword);

        Task<User> LogIntoAccountAsync(User user);
    }
}
