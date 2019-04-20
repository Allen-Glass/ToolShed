using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Models.Repository;
using ToolShed.Repository.Context;

namespace ToolShed.Repository.Repositories
{
    public class UserRepository
    {
        private readonly ToolShedContext toolShedContext;

        public UserRepository(ToolShedContext toolShedContext)
        {
            this.toolShedContext = toolShedContext;
        }

        public async Task<Guid> AddUserAsync(User user)
        {
            await toolShedContext
                .AddAsync(user);
            await toolShedContext.SaveChangesAsync();

            return user.UserId;
        }

        public async Task<bool> CheckIfUserEmailExists(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException();

            return await toolShedContext.UserSet
                .AnyAsync(c => c.Email.Equals(email));
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

        public async Task<string> GetHashedPasswordAsync(Guid userId)
        {
            return await toolShedContext.UserSet
                .Where(c => c.UserId.Equals(userId))
                .Select(c => c.Password)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            toolShedContext.UserSet
                .Update(user);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            toolShedContext.UserSet
                .Remove(user);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
