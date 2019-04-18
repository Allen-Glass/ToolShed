using System;
using System.Threading.Tasks;
using ToolShed.Models.API;

namespace ToolShed.Repository.Interfaces
{
    public interface IUserSQLService
    {
        /// <summary>
        /// Store user information in sql
        /// </summary>
        /// <param name="user">user infomraiton</param>
        Task StoreUserInformationAsync(User user);

        /// <summary>
        /// create user account
        /// </summary>
        /// <param name="user">user object</param>
        Task CreateNewUserAccount(User user);

        /// <summary>
        /// check if user email exists
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>true if user email</returns>
        Task<bool> CheckIfUserEmailExists(string email);

        /// <summary>
        /// Store credit card information
        /// </summary>
        /// <param name="card">credit card object</param>
        /// <param name="userId">pk of user</param>
        Task StoreCreditCardAsync(Card card, Guid userId);

        /// <summary>
        /// Delete user account information
        /// </summary>
        /// <param name="user">user object</param>
        /// <returns></returns>
        Task DeleteUserAccount(User user);
    }
}
