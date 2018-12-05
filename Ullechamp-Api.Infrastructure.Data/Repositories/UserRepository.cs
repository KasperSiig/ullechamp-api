using System.Collections.Generic;
using System.Linq;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UllechampContext _ctx;
        
        public UserRepository(UllechampContext ctx)
        {
            _ctx = ctx;
        }
        
        public User CreateUser(User user)
        {
            var userSaved = _ctx.Users.Add(user).Entity;
            _ctx.SaveChanges();
            return userSaved;
        }

        public IEnumerable<User> ReadAllUsers()
        {
            
            return _ctx.Users;
        }

        public User ReadById(int id)
        {
            return _ctx.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Delete(int id)
        {
            var userDeleted = _ctx.Remove(new User{Id = id}).Entity;
            _ctx.SaveChanges();
            return userDeleted;
        }

        public User Update(User user)
        {
            var userUpdate = _ctx.Update(user).Entity;
            _ctx.SaveChanges();
            return userUpdate;
        }

        public IEnumerable<User> ReadAllFiltered(Filter filter)
        {
            if (filter == null || filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
            {
                return _ctx.Users.OrderByDescending(x => x.Point);
            }

            return _ctx.Users.OrderByDescending(x => x.Point)
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage);
        }

        public int Count()
        {
            return _ctx.Users.Count();
        }
    }
}