using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;
using Queue = Ullechamp_Api.Core.Entity.Queue;

namespace Ullechamp_Api.Infrastructure.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly UllechampContext _ctx;
        private IUserRepository _userRepo;

        public TournamentRepository(UllechampContext ctx,
            IUserRepository userRepo)
        {
            _ctx = ctx;
            _userRepo = userRepo;
        }

        public IEnumerable<User> ReadUsersInQueue()
        {
            return _ctx.Queues.OrderBy(t => t.QueueTime).Select(q => q.User);
        }

        public void AddToQueue(string id, DateTime now)
        {
            var queueCheck = _ctx.Queues.FirstOrDefault(q => q.User.Id == int.Parse(id));
            if (queueCheck != null) return;
            var queue = new Queue()
            {
                User = new User() {Id = int.Parse(id)},
                QueueTime = now
            };
            _ctx.Attach(queue).State = EntityState.Added;
            _ctx.SaveChanges();
        }

        public void RemoveFromQueue(int id)
        {
            var queue = _ctx.Queues.FirstOrDefault(q => q.User.Id == id);
            _ctx.Remove(queue);
            _ctx.SaveChanges();
        }

        public void AddToCurrent(Tournament tournament, int userId, int team)
        {
            var user = _userRepo.ReadById(userId);
            var tournamentUser = new TournamentUser()
            {
                Tournament = tournament,
                User = user,
                Team = team
            };
            tournamentUser.TournamentId = tournament.Id;
            tournamentUser.UserId = userId;
            _ctx.TournamentUsers.Attach(tournamentUser).State = EntityState.Added;
            _ctx.SaveChanges();
        }

        public IEnumerable<TournamentUser> ReadUsersInCurrent()
        {
            var current = _ctx.Tournaments
                .Include(t => t.TournamentUsers)
                .FirstOrDefault(t => t.State == -1);
            if (current == null)
                return new List<TournamentUser>();

            return _ctx.TournamentUsers
                .Include(tu => tu.User)
                .Where(tu => tu.TournamentId == current.Id);
        }

        public IEnumerable<User> UpdateUsers(List<User> userList)
        {
            var updatedUsers = userList.Select(user => _ctx.Update(user).Entity);

            _ctx.SaveChanges();
            UpdateRank();
            return updatedUsers;
        }

        public IEnumerable<Tournament> ReadAllTournaments()
        {
            return _ctx.Tournaments;
        }

        public void UpdateState(int fromState, int toState)
        {
            _ctx.Tournaments
                .Where(t => t.State == fromState)
                .ToList()
                .ForEach(t => t.State = toState);
            _ctx.SaveChanges();
        }

        public IEnumerable<Tournament> ReadPending()
        {
            return _ctx.Tournaments.Where(t => t.State == 0);
        }

        public Tournament ReadPendingById(int id)
        {
            return _ctx.Tournaments.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TournamentUser> ReadUsersInPending(int id)
        {
            return _ctx.TournamentUsers
                .Include(tu => tu.User)
                .Where(tu => tu.TournamentId == id);
        }

        public Tournament Create()
        {
            var created = _ctx.Add(new Tournament()
            {
                DateTime = DateTime.Now,
                State = -1
            }).Entity;
            _ctx.SaveChanges();
            return created;
        }

        private void UpdateRank()
        {
            var counter = 1;
            var allUsers = _ctx.Users.OrderByDescending(x => x.Point);
            // Resets rank for everyone
            foreach (var oneUser in allUsers)
            {
                oneUser.Rank = counter++;
                // Notifies the context that oneUser has been modified
                _ctx.Attach(oneUser).State = EntityState.Modified;
            }

            _ctx.SaveChanges();
        }
    }
}