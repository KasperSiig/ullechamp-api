using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;
using Queue = Ullechamp_Api.Core.Entity.Queue;

namespace Ullechamp_Api.Infrastructure.Data.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly UllechampContext _ctx;

        public TournamentRepository(UllechampContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<User> ReadUsersInQueue()
        {
            var users = _ctx.Queues.OrderBy(t => t.QueueTime).Select(q => q.User);
            return users;
        }

        public void AddToQueue(string id, DateTime now)
        {
            var users = _ctx.Queues;

            var userId = users.FirstOrDefault(q => q.User.Id == int.Parse(id));
            if (userId != null) return;
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

        public void AddToCurrent(int id, int team, DateTime time, int tourId)
        {
            var current = new Tournament()
            {
                TournamentId = tourId,
                User = new User() {Id = id},
                State = -1,
                Team = team,
                DateTime = time
            };
            _ctx.Attach(current.User);
            _ctx.Attach(current).State = EntityState.Added;
            _ctx.SaveChanges();
        }

        public IEnumerable<Tournament> GetUsersInCurrent()
        {
            var allUsers = _ctx.Tournaments.Include(t => t.User);
            List<Tournament> current = new List<Tournament>();

            foreach (var user in allUsers)
            {
                if (user.State == -1)
                {
                    current.Add(user);
                }
            }

            return current;
        }

        public IEnumerable<User> UpdateUser(List<User> userList)
        {
            List<User> updatedUsers = new List<User>();
            foreach (var user in userList)
            {
                var updateUser = _ctx.Update(user).Entity;
                updatedUsers.Add(user);
            }

            _ctx.SaveChanges();
            UpdateRank();
            return updatedUsers;
        }

        public IEnumerable<Tournament> ReadAllTournaments()
        {
            return _ctx.Tournaments;
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