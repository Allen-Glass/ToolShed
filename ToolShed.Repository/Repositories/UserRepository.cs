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

        public async Task<Guid> AddAsync(User user)
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

        public async Task<User> GetAsync(string email)
        {
            return await toolShedContext.UserSet
                .FirstOrDefaultAsync(c => c.Email.Equals(email));
        }

        public async Task<User> GetAsync(Guid userId)
        {
            return await toolShedContext.UserSet
                .FirstOrDefaultAsync(c => c.UserId.Equals(userId));
        }

        public async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<Guid> userIds)
        {
            var userList = new List<User>();
            foreach(var userId in userIds)
            {
                var user = await GetAsync(userId);
                userList.Add(user);
            }

            return userList;
        }

        public async Task<IEnumerable<User>> ListAsync()
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

        public async Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            toolShedContext.UserSet
                .Update(user);
            await toolShedContext.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(Guid userId, string password)
        {
            var user = await GetAsync(userId);
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            toolShedContext.UserSet
                .Attach(user);
            toolShedContext.Entry(user).Property(c => c.Password).IsModified = true;
            await toolShedContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            toolShedContext.UserSet
                .Remove(user);
            await toolShedContext.SaveChangesAsync();
        }
    }
}
