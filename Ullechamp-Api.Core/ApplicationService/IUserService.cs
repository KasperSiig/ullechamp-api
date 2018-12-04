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
        
        #region GetAll
        /// <summary>
        /// Gets all users from the database
        /// </summary>
        /// <returns> A list of all users </returns>
        List<User> GetAllUsers();
        #endregion
    }
}