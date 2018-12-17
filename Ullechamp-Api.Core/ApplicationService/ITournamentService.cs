using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService
{
    public interface ITournamentService
    {
        List<User> GetUsersInQueue();

        void AddToQueue(string id);
        void RemoveFromQueue(int id);
        void AddToCurrent(int tourId, int id, int team);
        List<Tournament> GetUsersInCurrent();
        List<User> UpdateUser(List<User> updatedUser);
    }
}