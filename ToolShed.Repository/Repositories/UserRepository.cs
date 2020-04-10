using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await toolShedContext
                .AddAsync(user);
            await toolShedContext.SaveChangesAsync(cancellationToken);

            return user.UserId;
        }

        public async Task<bool> CheckIfUserEmailExists(string email, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException();

            return await toolShedContext.UserSet
                .AnyAsync(c => c.Email.Equals(email), cancellationToken);
        }

        public async Task<User> GetAsync(string email, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserSet
                .FirstOrDefaultAsync(c => c.Email.Equals(email), cancellationToken);
        }

        public async Task<User> GetAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserSet
                .FirstOrDefaultAsync(c => c.UserId.Equals(userId), cancellationToken);
        }

        public async Task<IEnumerable<User>> GetUsersAsync(IEnumerable<Guid> userIds, CancellationToken cancellationToken = default)
        {
            var userList = new List<User>();
            foreach(var userId in userIds)
            {
                var user = await GetAsync(userId, cancellationToken);
                userList.Add(user);
            }

            return userList;
        }

        public async Task<IEnumerable<User>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserSet
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllUserByFirstNameAsync(string firstName, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserSet
                .Where(c => c.FirstName.Equals(firstName))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllUserByLastNameAsync(string lastName, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserSet
                .Where(c => c.LastName.Equals(lastName))
                .ToListAsync(cancellationToken);
        }

        public async Task<string> GetHashedPasswordAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await toolShedContext.UserSet
                .Where(c => c.UserId.Equals(userId))
                .Select(c => c.Password)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user == null)
                throw new ArgumentNullException();

            toolShedContext.UserSet
                .Update(user);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdatePasswordAsync(Guid userId, string password, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            var user = await GetAsync(userId);

            toolShedContext.UserSet
                .Attach(user);
            toolShedContext.Entry(user).Property(c => c.Password).IsModified = true;
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            toolShedContext.UserSet
                .Remove(user);
            await toolShedContext.SaveChangesAsync(cancellationToken);
        }
    }
}
