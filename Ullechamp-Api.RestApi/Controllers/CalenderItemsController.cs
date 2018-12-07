using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CalenderItemsController : ControllerBase
    {
        private readonly ICalenderService _calenderService;
       
        /// <summary>
        /// Initialize CalenderService
        /// </summary>
        /// <param name="calenderService"></param>
        public CalenderItemsController(ICalenderService calenderService)
        {
            _calenderService = calenderService;
        }
        
        /// <summary>
        /// Get all calenderItems
        /// </summary>
        /// <returns>All CalenderItem</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CalenderItem>> Get()
        {
            return Ok(_calenderService.GetCalenders());
        }

        /// <summary>
        /// Creates a calenderItem
        /// </summary>
        /// <param name="calenderItem">CalenderItem to be created</param>
        /// <returns>Created CalenderItem</returns>
        [HttpPost]
        public ActionResult<CalenderItem> Post([FromBody] CalenderItem calenderItem)
        {
            return Ok(_calenderService.CreateCalender(calenderItem));
        }
    }
}