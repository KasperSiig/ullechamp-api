using System;
using System.Collections.Generic;
using System.Linq;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.DomainService
{
    public interface ITournamentRepository
    {
        IEnumerable<User> ReadUsersInQueue();
        
        void AddToQueue(string id, DateTime now);
        void RemoveFromQueue(int id);
        void AddToCurrent(int id, int team, DateTime time, int tourId);
        IEnumerable<Tournament> GetUsersInCurrent();
        IEnumerable<User> UpdateUser(List<User> userList);
        IEnumerable<Tournament> ReadAllTournaments();
        IEnumerable<User> ReadAllUsers();
        void UpdateTournament();
        IEnumerable<Tournament> ReadPending();
        Tournament ReadPendingById(int id);
        IEnumerable<Tournament> ReadUsersInPending(int id);
    }
}