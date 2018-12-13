using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService
{
    public interface ITournamentService
    {
        List<User> GetUsersInQueue();

        void AddToQueue(string id);
    }
}