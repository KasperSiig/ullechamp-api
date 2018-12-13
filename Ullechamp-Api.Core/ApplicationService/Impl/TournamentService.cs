using System;
using System.Collections.Generic;
using System.Linq;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService.Impl
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepo;

        public TournamentService(ITournamentRepository tournamentRepo)
        {
            _tournamentRepo = tournamentRepo;
        }
        
        public List<User> GetUsersInQueue()
        {
            return _tournamentRepo.ReadUsersInQueue().ToList();
        }

        public void AddToQueue(string id)
        {
            _tournamentRepo.AddToQueue(id, DateTime.Now);
        }

        public void RemoveFromQueue(int id)
        {
            _tournamentRepo.RemoveFromQueue(id);
        }

        public void AddToCurrent(int dad, int id, int team)
        {
            _tournamentRepo.AddToCurrent(dad, id, team);
        }

        public List<Tournament> GetUsersInCurrent()
        {
            return _tournamentRepo.GetUsersInCurrent().ToList();
        }
    }
}