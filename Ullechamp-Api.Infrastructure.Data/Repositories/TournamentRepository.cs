using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;

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
            var users = _ctx.Queues.Select(q => q.User);
            Console.WriteLine("user = " + users);
            return users;
        }

        public void AddToQueue(string id, DateTime now)
        {
            var queue = new Queue()
            {
                User = new User() {Id = int.Parse(id)},
                QueueTime = now
            };
            _ctx.Attach(queue).State = EntityState.Added;
            _ctx.SaveChanges();
        }
    }
}