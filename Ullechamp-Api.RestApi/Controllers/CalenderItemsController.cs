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

        public CalenderItemsController(ICalenderService calenderService)
        {
            _calenderService = calenderService;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CalenderItem>> Get()
        {
            return Ok(_calenderService.GetCalenders());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<CalenderItem> Post([FromBody] CalenderItem calenderItem)
        {
            return Ok(_calenderService.CreateCalender(calenderItem));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}