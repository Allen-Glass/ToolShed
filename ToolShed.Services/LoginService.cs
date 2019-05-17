using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ToolShed.Models.API;
using ToolShed.Repository.Interfaces;
using ToolShed.Services.Interfaces;

namespace ToolShed.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserDataService userSQLService;
        private readonly IEmailService emailService;

        public LoginService(IUserDataService userSQLService,
            IEmailService emailService)
        {
            this.userSQLService = userSQLService;
            this.emailService = emailService;
        }

        public async Task CreateNewAccountAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            var userEmailAlreadyExist = await userSQLService.CheckIfUserEmailExists(user.Email);

            if (userEmailAlreadyExist)
            {
                throw new Exception();
            }

            user.Password = HashUserPassword(user.Password);

            await userSQLService.CreateNewUserAccountAsync(user);
        }

        public async Task<User> GetUserInformationAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            return await userSQLService.GetUserInformationAsync(userId);
        }

        public async Task SendPasswordResetEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            var emailDoesExist = await userSQLService.CheckIfUserEmailExists(email);
            if (!emailDoesExist)
                throw new NullReferenceException(nameof(email));

            await emailService.SendPasswordResetEmailAsync(email);
        }

        public async Task UpdateUserPasswordAsync(Guid userId, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException();

            await userSQLService.UpdateUserPasswordAsync(userId, newPassword);
        }

        /// <summary>
        /// TODO: JOSH FIGURE THIS SHIT OUT thanks
        /// </summary>
        /// <param name="userSecret"></param>
        /// <returns></returns>
        private string HashUserPassword(string userSecret)
        {
            if (string.IsNullOrEmpty(userSecret))
            {
                throw new ArgumentNullException();
            }

            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userSecret,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public async Task<User> LogIntoAccountAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            var accountEmailExists = await userSQLService.CheckIfUserEmailExists(user.Email);

            if (!accountEmailExists)
                throw new NullReferenceException(nameof(accountEmailExists));

            var dtoHashedPassword = await userSQLService.GetHashedPasswordAsync(user.UserId);
            var passwordMatches = HashUserPassword(user.Password) == dtoHashedPassword;

            if (!passwordMatches)
                throw new NullReferenceException(nameof(dtoHashedPassword));

            user = await userSQLService.GetUserInformationAsync(user.UserId);
            //user needs to be assigned a jwt

            return user;
        }
    }
}
