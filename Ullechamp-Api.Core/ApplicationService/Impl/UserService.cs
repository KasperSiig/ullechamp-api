using System.Collections.Generic;
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
    }
}