using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            var userDeleted = _ctx.Remove(new User {Id = id}).Entity;
            _ctx.SaveChanges();
            return userDeleted;
        }

        public User Update(User user)
        {
            var userUpdate = _ctx.Update(user).Entity;
            _ctx.SaveChanges();
            UpdateRank();
            return userUpdate;
        }


        public IEnumerable<User> ReadAllFiltered(Filter filter)
        {
            return ReadFiltered(filter, _ctx.Users);
        }

        public IEnumerable<User> ReadSearchFiltered(Filter filter, string search)
        {
            // Find all users matching the search query
            var searchResult = _ctx.Users.ToList().FindAll(x => x.Username.ToLower()
                .Contains(search.ToLower()));
            
            return ReadFiltered(filter, searchResult);
        }

        private IEnumerable<User> ReadFiltered(Filter filter, IEnumerable<User> users)
        {
            if (filter == null || filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
                return _ctx.Users.OrderByDescending(x => x.Point);
        
            // Skips previous pages and returns next page
            return users.OrderByDescending(x => x.Point)
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage);
        }

        public int Count()
        {
            return _ctx.Users.Count();
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