using Firebase.Auth;
using Firebase.Auth.Payloads;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WebshopAPI.Core.Entity;
using WebshopAPI.Core.DomainService;

namespace WebshopAPI.Infrastructure.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public async Task<User> RegisterUser(LoginInput User)
        {
            User user = new User();
            var authOptions = new FirebaseAuthOptions("AIzaSyDHXodPd4jjaXSzwxrdCzJMptaiMPNeYME");
            var firebase = new FirebaseAuthService(authOptions);

            // Set up DAta
            var request = new SignUpNewUserRequest
            {
                Email = User.Email,
                Password = User.Password
            };

            // Execute register query
            SignUpNewUserResponse response = await firebase.SignUpNewUser(request);

            // Extract data
            user.Email = response.Email;
            user.IdToken = response.IdToken;
            user.ExpiresIn = response.ExpiresIn;
            user.LocalId = response.LocalId;

            return user;
        }

        public async Task<User> LoginUser(LoginInput User)
        {

            var authOptions = new FirebaseAuthOptions("AIzaSyDHXodPd4jjaXSzwxrdCzJMptaiMPNeYME");
            var firebase = new FirebaseAuthService(authOptions);

            User user = new User();

            // Set up data
            var request = new VerifyPasswordRequest()
            {
                Email = User.Email,
                Password = User.Password
            };

            // Execute login query
            var response = await firebase.VerifyPassword(request);

            // Get Data
            user.Email = response.Email;
            user.IdToken = response.IdToken;
            user.ExpiresIn = response.ExpiresIn;
            user.LocalId = response.LocalId;

            return user;

        }
    }
}
