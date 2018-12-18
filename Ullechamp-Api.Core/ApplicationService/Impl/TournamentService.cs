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
            var time = DateTime.Now;
            var firstTour = _tournamentRepo.ReadAllTournaments().OrderByDescending(t => t.TournamentId).First();
            int tourId = firstTour == null ? 1 : firstTour.TournamentId++;
            var check = _tournamentRepo.ReadAllTournaments().FirstOrDefault(t => t.State == -1);
            if (check != null)
            {
                time = check.DateTime;
                tourId = check.TournamentId;
            }
            _tournamentRepo.AddToCurrent(userId, team, time, tourId);
        }

        public List<Tournament> GetUsersInCurrent()
        {
            return _tournamentRepo.GetUsersInCurrent().ToList();
        }

        public List<User> UpdateUser(List<User> updatedUser)
        {
            List<User> userList = new List<User>();
            
            foreach (var user in updatedUser)
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
                user.WinLoss = Convert.ToInt32((winDouble / (winDouble + lossDouble)) * 100);
                user.Point = Convert.ToInt32(Math.Round(pointResult, 0));
                userList.Add(user);
            }

            return _tournamentRepo.UpdateUser(userList).ToList();
        }

        public void UpdateState()
        {
            _tournamentRepo.UpdateTournament();
        }
    }
}