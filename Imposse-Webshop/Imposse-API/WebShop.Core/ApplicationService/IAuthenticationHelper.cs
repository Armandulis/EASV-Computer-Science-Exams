using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.ApplicationService.Services
{
    public interface IAuthenticationHelper
    {
        /// <summary>
        /// Generate a password hash and salt using the provided password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        /// <summary>
        /// Verify whether the given password matches the stored hash and salt.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);

        /// <summary>
        /// Generates a JWT security token for the given user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GenerateToken(User user);
    }
}
