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
        Task AddUserInformationAsync(User user);

        /// <summary>
        /// create user account
        /// </summary>
        /// <param name="user">user object</param>
        Task CreateNewUserAccountAsync(User user);

        /// <summary>
        /// get hashed password
        /// </summary>
        /// <param name="userId">pk of user</param>
        /// <returns>hashed password</returns>
        Task<string> GetHashedPasswordAsync(Guid userId);

        /// <summary>
        /// get all information associated with a user
        /// </summary>
        /// <param name="userId">pk of user</param>
        /// <returns>user object</returns>
        Task<User> GetUserInformationAsync(Guid userId);

        /// <summary>
        /// update user account information
        /// </summary>
        /// <param name="user">user object</param>
        Task UpdateUserAccountAsync(User user);

        /// <summary>
        /// delete user account
        /// </summary>
        /// <param name="user">user object</param>
        Task DeleteUserAccount(User user);

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
        Task AddCardAsync(Card card, Guid userId);
    }
}
