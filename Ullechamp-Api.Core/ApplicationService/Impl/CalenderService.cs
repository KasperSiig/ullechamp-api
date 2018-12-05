using System.Collections.Generic;
using System.Linq;
using System.Net;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService.Impl
{
    public class CalenderService : ICalenderService
    {
        private readonly ICalenderRepository _calRepo;

        public CalenderService(ICalenderRepository calRepo)
        {
            _calRepo = calRepo;
        }
        
        public CalenderItem CreateCalender(CalenderItem calenderItem)
        {
           return _calRepo.CreateCalender(calenderItem);
        }
        
        public List<CalenderItem> GetCalenders()
        {
            var calenders = _calRepo.ReadCalender();
            return calenders.ToList();
        }
    }
}