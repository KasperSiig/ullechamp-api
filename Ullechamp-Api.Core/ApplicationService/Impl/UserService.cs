using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        
        public User CreateUser(User user)
        {
            
            return _userRepo.CreateUser(user);
        }

        public List<User> GetAllUsers()
        {
            var users = _userRepo.ReadAllUsers();
            return users.ToList();
        }

        public User GetById(int id)
        {
            return _userRepo.ReadById(id);
        }

        public User Delete(int id)
        {
            return _userRepo.Delete(id);
        }

        public User Update(User user)
        {
            var updateUser = _userRepo.Update(user);
            
            
            return updateUser;
        }

        public List<User> GetFilteredList(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
            {
                throw new InvalidDataException("CurrentPage and ItemsPrPage must be zero or more");
            }

            if ((filter.CurrentPage - 1 * filter.ItemsPrPage) >= _userRepo.Count())
            {
                throw new InvalidDataException("Index out of bounds, CurrentPage is to high");
            }

            return _userRepo.ReadAllFiltered(filter).ToList();
        }

        public List<User> SearchList(Filter filter, string searchQuery)
        {
            return _userRepo.ReadSearchFiltered(filter, searchQuery).ToList();
        }
    }
}