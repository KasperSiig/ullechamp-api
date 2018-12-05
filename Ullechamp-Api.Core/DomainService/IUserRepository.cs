using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.DomainService
{
    public interface IUserRepository
    {
        
        #region Create
        /// <summary>
        /// Creates a user in the database
        /// </summary>
        /// <param name="user"> User to be inserted in Database</param>
        /// <returns> Created user </returns>
        User CreateUser(User user);
        #endregion
        
        #region ReadAll
        /// <summary>
        /// Reads all users from the database
        /// </summary>
        /// <returns> A IEnumerable of all users </returns>
        IEnumerable<User> ReadAllUsers();
        #endregion

        #region ReadById
        /// <summary>
        /// Reads a user by id
        /// </summary>
        /// <param name="id">The id of the user to be read</param>
        /// <returns>The user</returns>
        User ReadById(int id);
        #endregion

        #region Delete
        /// <summary>
        /// Deletes user by id
        /// </summary>
        /// <param name="id"> The id of the user to be deleted</param>
        /// <returns>The deleted user</returns>
        User Delete(int id);
        #endregion

        #region Update
        /// <summary>
        /// Updates user by id from database
        /// </summary>
        /// <param name="id">Id of user to be updated</param>
        /// <returns>Updated user</returns>
        User Update(User user);
        #endregion
    }
}