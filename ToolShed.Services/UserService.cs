using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Services
{
    public class UserService
    {
        public UserService()
        {

        }

        public async Task AddUserAsync(User user, CancellationToken cancellation = default)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));


        }

        public async Task ViewOwnerInformationAsync(User user, CancellationToken cancellation = default)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));


        }

        public async Task 
    }
}
