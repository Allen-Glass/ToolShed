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
        private readonly IUserSQLService userSQLService;

        public LoginService(IUserSQLService userSQLService)
        {
            this.userSQLService = userSQLService;
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

            await userSQLService.CreateNewUserAccount(user);
        }

        public async Task UpdatePasswordAsync()
        {

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

        public async Task LogIntoAccountAsync(User user)
        {

        }
    }
}
