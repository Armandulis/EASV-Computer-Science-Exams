using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.ApplicationService
{
    public interface IUserService
    {
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User NewUser(User user);

        /// <summary>
        /// Delete a user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User DeleteUser(int id);

        /// <summary>
        /// Get a list of all users.
        /// </summary>
        /// <returns></returns>
        List<User> ReadAllUsers();

        /// <summary>
        /// Get a user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User FindUserByID(int id);

        /// <summary>
        /// Update a user with new information.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User UpdateUser(User user);

        /// <summary>
        /// Get a user by username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetByUsername(string username);

        /// <summary>
        /// Populates the user with its password hash and salt
        /// using the password info in the login model.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        void SetPasswordInfo(User user, LoginInputModel login);

        /// <summary>
        /// Get a list of all usernames in the database.
        /// </summary>
        /// <returns></returns>
        List<string> GetUsernames();     
    }
}
