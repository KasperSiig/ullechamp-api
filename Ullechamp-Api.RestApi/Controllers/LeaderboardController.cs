using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;
using Ullechamp_Api.RestApi.Dtos;

namespace Ullechamp_Api.RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly IUserService _userService;

        public LeaderboardController(IUserService userService)
        {
            _userService = userService;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get([FromQuery]Filter filter)
        {
            var rankCount = filter.CurrentPage == 1 
                ? 1 
                : (filter.CurrentPage - 1) * (filter.ItemsPrPage + 1);
            var sortedLists = _userService.GetFilteredList(filter);
            var rankList = new List<UserDTO>();
            foreach (var sortedList in sortedLists)
            {
                var userDto = new UserDTO()
                {
                    User = sortedList,
                    Rank = rankCount++
                };
                rankList.Add(userDto);
            }
            
            
            return rankList;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.GetById(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            return Ok(_userService.CreateUser(user));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            if (!id.Equals(user.Id))
            {
                return BadRequest("Parameter ID and User ID must be the same!");
            }

            return _userService.Update(user);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            return _userService.Delete(id);
        }
    }
}