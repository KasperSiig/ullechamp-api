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
    }
}