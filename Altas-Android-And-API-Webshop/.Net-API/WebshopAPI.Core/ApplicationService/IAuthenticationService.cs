
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebshopAPI.Core.Entity;


namespace WebshopAPI.Core.ApplicationService
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Calls repo to register user
        /// </summary>
        /// <param name="loginInput">user's login input</param>
        /// <returns>Loged in user</returns>
        User RegisterUser(LoginInput loginInput);

        /// <summary>
        /// Calls repo to login user
        /// </summary>
        /// <param name="loginInput">user's login input</param>
        /// <returns>Loged in user</returns>
        User LoginUser(LoginInput loginInput);

    }
}
