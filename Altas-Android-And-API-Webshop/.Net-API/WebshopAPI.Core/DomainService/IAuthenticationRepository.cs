using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebshopAPI.Core.Entity;

namespace WebshopAPI.Core.DomainService
{
    public interface IAuthenticationRepository
    {

        /// <summary>
        /// Register user in firebaseAuth
        /// </summary>
        /// <param name="loginInput">User's login input</param>
        /// <returns>Loged in user</returns>
        Task<User> RegisterUser(LoginInput loginInput);

        /// <summary>
        /// Login user with firebaseAuth
        /// </summary>
        /// <param name="loginInput">User's login input</param>
        /// <returns>Loged in user</returns>
        Task<User> LoginUser(LoginInput loginInput);
    };
}
