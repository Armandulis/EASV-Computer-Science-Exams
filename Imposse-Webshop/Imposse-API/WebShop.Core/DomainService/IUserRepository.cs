using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Core.Entity;

namespace WebShop.Core.DomainService
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get all users from the database.
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Get a user from the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User UserGet(int id);

        /// <summary>
        /// Add a new user to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        User UserAdd(User entity);

        /// <summary>
        /// Update a user with new information.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        User UserEdit(User entity);

        /// <summary>
        /// Delete a user from the database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User UserRemove(int id);
    }
}