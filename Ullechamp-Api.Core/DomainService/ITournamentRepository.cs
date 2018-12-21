using System;
using System.Collections.Generic;
using System.Linq;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.DomainService
{
    public interface ITournamentRepository
    {
        #region Create

        /// <summary>
        /// Adds User to queue
        /// </summary>
        /// <param name="id">Id of User to add</param>
        /// <param name="now">Current Timestamp</param>
        void AddToQueue(string id, DateTime now);

        /// <summary>
        /// Add User to current tournament
        /// </summary>
        /// <param name="tournament">Tournament to add to</param>
        /// <param name="userId">Id of user to add to</param>
        /// <param name="team">Team to add user to</param>
        void AddToCurrent(Tournament tournament, int userId, int team);

        /// <summary>
        /// Create tournament
        /// </summary>
        /// <returns>Created tournament</returns>
        Tournament Create();

        #endregion

        #region Read

        /// <summary>
        /// Read all users in queue
        /// </summary>
        /// <returns>Users in queue</returns>
        IEnumerable<User> ReadUsersInQueue();

        /// <summary>
        /// Reads all users in current tournament
        /// </summary>
        /// <returns>All users in current tournament</returns>
        IEnumerable<TournamentUser> ReadUsersInCurrent();

        /// <summary>
        /// Reads all tournaments
        /// </summary>
        /// <returns>List of tournaments</returns>
        IEnumerable<Tournament> ReadAllTournaments();

        /// <summary>
        /// Reads pending tournaments
        /// </summary>
        /// <returns>Pending tournaments</returns>
        IEnumerable<Tournament> ReadPending();

        /// <summary>
        /// Read pending by id
        /// </summary>
        /// <param name="id">Id of pending tournament to read</param>
        /// <returns>Tournament</returns>
        Tournament ReadPendingById(int id);

        /// <summary>
        /// Reads users in pending tournament
        /// </summary>
        /// <param name="id">Id of tournament to read</param>
        /// <returns>List of Users</returns>
        IEnumerable<TournamentUser> ReadUsersInPending(int id);

        #endregion

        #region Update

        /// <summary>
        /// Updates users KDA, points and rank
        /// </summary>
        /// <param name="userList">List of users to be updated</param>
        /// <returns>List of users</returns>
        IEnumerable<User> UpdateUsers(List<User> userList);

        /// <summary>
        /// Updates state of tournament
        /// </summary>
        /// <param name="fromState">State from which to change</param>
        /// <param name="toState">State to turn to</param>
        void UpdateState(int fromState, int toState);

        #endregion

        #region Delete

        /// <summary>
        /// Removes user for queue
        /// </summary>
        /// <param name="id"></param>
        void RemoveFromQueue(int id);

        #endregion
    }
}