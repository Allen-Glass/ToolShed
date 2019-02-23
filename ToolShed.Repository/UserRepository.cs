using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.User;
using ToolShed.Repository.Context;

namespace ToolShed.Repository
{
    public class UserRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task AddUserAsync(User userInformation)
        {
            await toolShedContext.AddAsync(userInformation);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await toolShedContext.UserSet
                .FirstOrDefaultAsync(c => c.Email.Equals(email));
        }

        public async Task<User> GetUserByUserIdAsync(Guid userId)
        {
            return await toolShedContext.UserSet
                .FirstOrDefaultAsync(c => c.UserId.Equals(userId));
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await toolShedContext.UserSet
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUserByFirstNameAsync(string firstName)
        {
            return await toolShedContext.UserSet
                .Where(c => c.FirstName.Equals(firstName))
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUserByLastNameAsync(string lastName)
        {
            return await toolShedContext.UserSet
                .Where(c => c.LastName.Equals(lastName))
                .ToListAsync();
        }

        public async Task UpdateUserAddressAsync(User user)
        {
            toolShedContext.UserSet
                .Where(c => c.UserId.Equals(user.UserId))
                .ToList()
                .ForEach(c => c.Address = user.Address);

            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            toolShedContext.Remove(user);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
