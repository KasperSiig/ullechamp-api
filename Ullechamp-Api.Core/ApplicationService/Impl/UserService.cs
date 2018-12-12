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
            return _userRepo.ReadAllUsers().ToList();
        }

        public User GetById(int id)
        {
            return _userRepo.ReadById(id);
        }

        public User Delete(int id)
        {
            return _userRepo.Delete(id);
        }

        public List<User> GetFilteredStats(Filter filter, string sorting)
        {
            switch (sorting)
            {
                case "Wins":
                    return _userRepo.ReadAllFiltered(filter).OrderByDescending(x => x.Wins).ToList();
                case "Losses":
                    return _userRepo.ReadAllFiltered(filter).OrderByDescending(x => x.Losses).ToList();
                case "Kills":
                   return  _userRepo.ReadAllFiltered(filter).OrderByDescending(x => x.Kills).ToList();
                case "Deaths":
                    return _userRepo.ReadAllFiltered(filter).OrderByDescending(x => x.Deaths).ToList();
                case "Assists":
                    return _userRepo.ReadAllFiltered(filter).OrderByDescending(x => x.Assists).ToList();
                case "Kda":
                    return _userRepo.ReadAllFiltered(filter).OrderByDescending(x => x.Kda).ToList();
                case "W/L":
                    return _userRepo.ReadAllFiltered(filter).OrderByDescending(x => x.WinLoss).ToList();
                case "Point":
                    return _userRepo.ReadAllFiltered(filter).OrderByDescending(x => x.Point).ToList();
                default:
                    return _userRepo.ReadAllFiltered(filter).ToList();
            }
        }

        public User GetUserByTwitchId(int userId)
        {
            return _userRepo.ReadUserByTwitchId(userId);
        }

        public User Update(User user)
        {
            return _userRepo.Update(user);
        }

        public List<User> GetFilteredList(Filter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
                throw new InvalidDataException("CurrentPage and ItemsPrPage must be zero or more");

            if ((filter.CurrentPage - 1 * filter.ItemsPrPage) >= _userRepo.Count())
                throw new InvalidDataException("Index out of bounds, CurrentPage is too high");

            return _userRepo.ReadAllFiltered(filter).ToList();
        }

        public List<User> SearchList(Filter filter, string searchQuery)
        {
            if (searchQuery == null)
                GetAllUsers();

            return _userRepo.ReadSearchFiltered(filter, searchQuery).ToList();
        }
    }
}