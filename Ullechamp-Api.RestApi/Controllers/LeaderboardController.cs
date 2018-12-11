using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly IUserService _userService;
       
        /// <summary>
        /// Initialize LeaderboardController
        /// </summary>
        /// <param name="userService"></param>
        public LeaderboardController(IUserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// Gets all Users
        /// </summary>
        /// <param name="filter">Filter to implement pagination</param>
        /// <returns>Filtered Users</returns>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get([FromQuery]Filter filter)
        {
            return Ok(_userService.GetFilteredList(filter));
        }

        /// <summary>
        /// Gets all users in specific order
        /// </summary>
        /// <param name="sorting">What to be sorted by</param>
        /// <param name="filter">The Filter properties</param>
        /// <returns>List of sorted Users</returns>
        [HttpGet("stats")]
        public ActionResult<IEnumerable<User>> Get(string sorting, [FromQuery]Filter filter)
        {
            return Ok(_userService.GetFilteredStats(filter, sorting));
        }

        /// <summary>
        /// Gets list of searched Users
        /// </summary>
        /// <param name="filter">The filter properties</param>
        /// <param name="search">Search String</param>
        /// <returns>List of filtered Users</returns>
        [HttpGet("search")]
        public ActionResult<IEnumerable<User>> Get([FromQuery]Filter filter, string search)
        {
            return Ok(_userService.SearchList(filter, search));
        }

        /// <summary>
        /// Gets User by id 
        /// </summary>
        /// <param name="id">Id of User</param>
        /// <returns>The User with the specified id</returns>
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

        /// <summary>
        /// Creates a User
        /// </summary>
        /// <param name="user">The Users properties</param>
        /// <returns>Created User</returns>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            return Ok(_userService.CreateUser(user));
        }

        /// <summary>
        /// Updates selected User
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <param name="user">Properties of the User</param>
        /// <returns>Updated User</returns>
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            if (!id.Equals(user.Id))
                return BadRequest("Parameter ID and User ID must be the same!");

            return Ok(_userService.Update(user));
        }

        /// <summary>
        /// Deletes User
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>Ok</returns>
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            return Ok(_userService.Delete(id));
        }
    }
}