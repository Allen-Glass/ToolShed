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

        Task UpdatePasswordAsync();

        Task LogIntoAccountAsync(User user);
    }
}
