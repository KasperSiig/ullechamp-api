using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService
{
    public interface IUserService
    {
        #region Create
        /// <summary>
        /// Creates a user in the database
        /// </summary>
        /// <param name="user"> User to be inserted in the database </param>
        /// <returns> Created user</returns>
        User CreateUser(User user);
        #endregion
        
        #region Read
        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns> A list of all users </returns>
        List<User> GetAllUsers();
        
        /// <summary>
        /// Gets a user by id from the database
        /// </summary>
        /// <param name="id">The id of the user to be read</param>
        /// <returns>The user</returns>
        User GetById(int id);
        
        /// <summary>
        /// Gets the filtered list of users
        /// </summary>
        /// <param name="filter">The filter properties</param>
        /// <returns>The filtered list of users</returns>
        List<User> GetFilteredList(Filter filter);
        
        /// <summary>
        /// Searches all Users based on a searchQuery
        /// </summary>
        /// <param name="filter">The filter properties</param>
        /// <param name="searchQuery">Search String</param>
        /// <returns>A list of filtered Users</returns>
        List<User> SearchList(Filter filter, string searchQuery);
        
        /// <summary>
        /// Gets all Users in a specific order
        /// </summary>
        /// <param name="filter">The filter properties</param>
        /// <param name="stats">The sorting chosen</param>
        /// <returns>A list of sorted Users</returns>
        List<User> GetFilteredStats(Filter filter, string sorting);
        #endregion

        #region Update
        /// <summary>
        /// Updates user by id from database
        /// </summary>
        /// <param name="id">Id of user to be updated</param>
        /// <returns>Updated user</returns>
        User Update(User user);
        #endregion
        
        #region Delete
        /// <summary>
        /// Deletes user by id
        /// </summary>
        /// <param name="id">Id of user to be deleted</param>
        /// <returns>The user that has been deleted</returns>
        User Delete(int id);
        #endregion


        
    }
}