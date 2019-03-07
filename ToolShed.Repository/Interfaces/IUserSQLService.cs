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
