using System;
using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.DomainService
{
    public interface ITournamentRepository
    {
        IEnumerable<User> ReadUsersInQueue();
        
        void AddToQueue(string id, DateTime now);
    }
}