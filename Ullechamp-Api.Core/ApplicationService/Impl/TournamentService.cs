using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
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

        public void AddToCurrent(int userId, int team)
        {
            var tour = _tournamentRepo.ReadAllTournaments().
                           FirstOrDefault(t => t.State == -1) ?? Create();

            _tournamentRepo.AddToCurrent(tour, userId, team);
        }

        private Tournament Create()
        {
            return _tournamentRepo.Create();
        }
        

        public List<TournamentUser> GetUsersInCurrent()
        {
            return _tournamentRepo.ReadUsersInCurrent().ToList();
        }

        public List<User> UpdateUsers(List<User> updatedUsers)
        {
            List<User> userList = new List<User>();
            
            foreach (var user in updatedUsers)
            {
                var killDouble = Convert.ToDouble(user.Kills);
                var deathDouble = Convert.ToDouble(user.Deaths);
                var assistDouble = Convert.ToDouble(user.Assists);
                var winDouble = Convert.ToDouble(user.Wins);
                var lossDouble = Convert.ToDouble(user.Losses);
                
                var pointResult = (((winDouble) * 10) +
                                   ((winDouble) + ((killDouble + assistDouble) / (deathDouble)) * 2) -
                                   (lossDouble * 3)) * 2;
                var kdaResult = (killDouble + assistDouble) / deathDouble;
                
                user.Kda = Math.Round(kdaResult, 1);
                user.WinLoss = user.Wins.Equals(0) 
                    ? 0 
                    : Convert.ToInt32((winDouble / (winDouble + lossDouble)) * 100);
                
                user.Point = Convert.ToInt32(Math.Round(pointResult, 0));
                userList.Add(user);
            }
            
            UpdateState(0, 1);
            return _tournamentRepo.UpdateUsers(userList).ToList();
        }

        public void UpdateState(int fromState, int toState)
        {
            _tournamentRepo.UpdateState(fromState, toState);
        }

        public List<Tournament> GetPending()
        {
            return _tournamentRepo.ReadPending().ToList();
        }

        public Tournament GetPendingById(int id)
        {
            return _tournamentRepo.ReadPendingById(id);
        }

        public List<TournamentUser> GetUsersInPending(int id)
        {
            return _tournamentRepo.ReadUsersInPending(id).ToList();
        }
    }
}