using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService
{
    public interface ITournamentService
    {
        #region Create

        /// <summary>
        /// Add User to current tournament
        /// </summary>
        /// <param name="userId">User to add</param>
        /// <param name="team">Team to add user to</param>
        void AddToCurrent(int userId, int team);
        
        /// <summary>
        /// Add User to queue
        /// </summary>
        /// <param name="id">Id of User to add</param>
        void AddToQueue(string id);

        #endregion

        #region Read

        /// <summary>
        /// Gets users in queue
        /// </summary>
        /// <returns>Users in queue</returns>
        List<User> GetUsersInQueue();
        
        /// <summary>
        /// Gets users in current tournament
        /// </summary>
        /// <returns>Users in current tournament</returns>
        List<TournamentUser> GetUsersInCurrent();
        
        /// <summary>
        /// Get tournaments pending to have KDA entered
        /// </summary>
        /// <returns>Pending tournaments</returns>
        List<Tournament> GetPending();
        
        /// <summary>
        /// Gets pending tournament by id
        /// </summary>
        /// <param name="id">Id to get pending tournament by</param>
        /// <returns></returns>
        Tournament GetPendingById(int id);
        
        /// <summary>
        /// Get users in pending tournament
        /// </summary>
        /// <param name="id">Id to get users by</param>
        /// <returns>List of users</returns>
        List<TournamentUser> GetUsersInPending(int id);

        #endregion


        #region Update

        /// <summary>
        /// Changes state from one to another
        /// </summary>
        /// <param name="fromState">State to change from</param>
        /// <param name="toState">State to change to</param>
        void UpdateState(int fromState, int toState);
        
        /// <summary>
        /// Updates users KDA and rank
        /// </summary>
        /// <param name="updatedUsers">List of users to be updated</param>
        /// <returns>List of users</returns>
        List<User> UpdateUsers(List<User> updatedUsers);

        #endregion

        #region Delete

        /// <summary>
        /// Removes user from queue
        /// </summary>
        /// <param name="id"></param>
        void RemoveFromQueue(int id);

        #endregion
    }
}