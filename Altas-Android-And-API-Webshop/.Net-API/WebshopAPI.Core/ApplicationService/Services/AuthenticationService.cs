using System;
using System.Collections.Generic;
using System.Text;
using WebshopAPI.Core.Entity;
using WebshopAPI.Core.DomainService;
using System.Threading.Tasks;

namespace WebshopAPI.Core.ApplicationService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        /** AuthenticationService constructor with DI**/
        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        /** Calls repo to login user **/
        public User LoginUser(LoginInput loginInput)
        {
            // Log in user
            return _authenticationRepository.LoginUser(loginInput).Result;
        }

        /** Calls repo to register user **/
        public User RegisterUser(LoginInput loginInput)
        {
            // Register user
            return _authenticationRepository.RegisterUser(loginInput).Result;
        }


    }
}
