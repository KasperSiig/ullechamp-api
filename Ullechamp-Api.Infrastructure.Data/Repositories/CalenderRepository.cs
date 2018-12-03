using System.Collections.Generic;
using Ullechamp_Api.Core.DomainService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Infrastructure.Data.Repositories
{
    public class CalenderRepository : ICalenderRepository
    {
        private readonly UllechampContext _ctx;

        public CalenderRepository(UllechampContext ctx)
        {
            _ctx = ctx;
        }
        
        public CalenderItem CreateCalender(CalenderItem calenderItem)
        {
            var calSaved = _ctx.Calenders.Add(calenderItem).Entity;
            _ctx.SaveChanges();
            return calSaved;
        }

        public IEnumerable<CalenderItem> ReadCalender()
        {
            return _ctx.Calenders;
        }
    }
}