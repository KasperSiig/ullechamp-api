using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;
using Ullechamp_Api.RestApi.Dtos;

namespace Ullechamp_Api.RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly IUserService _userService;
        private readonly IConfiguration _conf;
        
        public TournamentController(ITournamentService tournamentService, IUserService userService, IConfiguration conf)
        {
            _tournamentService = tournamentService;
            _userService = userService;
            _conf = conf;
        }
        
        
        [HttpGet("Queue")]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_tournamentService.GetUsersInQueue());
        }

        [HttpGet("Current")]
        public ActionResult<IEnumerable<TournamentDTO>> GetCurrent()
        {
            var list = _tournamentService.GetUsersInCurrent();
            var list2 = new List<TournamentDTO>();

            foreach (var tournament in list)
            {
                list2.Add(new TournamentDTO()
                {
                    TournamentId = tournament.Id,
                    User = tournament.User,
                    Team = tournament.Team
                });
            }
            return Ok(list2);
        }

        
        [HttpPost("Queue")]
        public ActionResult<JObject> Post([FromBody]JObject jObject)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtDecoded = handler.ReadJwtToken(jObject.GetValue("jwt").ToString());
            //TODO check for null
            var id = jwtDecoded.Claims.FirstOrDefault((p => p.Type == "id")).Value;
            _tournamentService.AddToQueue(id);
            
            return Ok(id);
        }

        [HttpPost("Current")]
        public ActionResult<Tournament> Post([FromBody] TournamentDTO tournamentDto)
        {
            var id = tournamentDto.User.Id;
            var team = tournamentDto.Team;
            var tourList = _tournamentService.GetUsersInCurrent().OrderBy(t => t.TournamentId);
            var tourId = 1;
            //var tourId = tourList.First().TournamentId;
            tourId++;
            _tournamentService.AddToCurrent(tourId, id, team);
            _tournamentService.RemoveFromQueue(id);
            return Ok();
        }
        
        [HttpPut("Winners")]
        public ActionResult<IEnumerable<UserDTO>> Put([FromBody] UserDTO userDto)
        {
            List<User> updatedUser = new List<User>(); 
            
            foreach (var user in userDto.User)
            {
                var id = user.Id;
                var oldUser = _userService.GetById(id);
                var userKills = oldUser.Kills + user.Kills;
                var userDeaths = oldUser.Deaths + user.Deaths;
                var userAssists = oldUser.Assists + user.Assists;
                var userWins = oldUser.Wins + user.Wins;
                oldUser.Kills = userKills;
                oldUser.Deaths = userDeaths;
                oldUser.Assists = userAssists;
                oldUser.Wins = userWins;
                updatedUser.Add(oldUser);
            }
            
            

            return Ok(_tournamentService.UpdateUser(updatedUser));
        }

        [HttpPut("Losers")]
        public ActionResult<IEnumerable<UserDTO>> PutLosers([FromBody] UserDTO userDto)
        {
            List<User> updatedUser = new List<User>();

            foreach (var user in userDto.User)
            {
                var id = user.Id;
                var oldUser = _userService.GetById(id);
                var userKills = oldUser.Kills + user.Kills;
                var userDeaths = oldUser.Deaths + user.Deaths;
                var userAssists = oldUser.Assists + user.Assists;
                var userLosses = oldUser.Losses + user.Losses;
                oldUser.Kills = userKills;
                oldUser.Deaths = userDeaths;
                oldUser.Assists = userAssists;
                oldUser.Losses = userLosses;
                updatedUser.Add(oldUser);
            }

            return Ok(_tournamentService.UpdateUser(updatedUser));
        }

        [HttpDelete("Queue/{id}")]
        public ActionResult Delete(int id)
        {
            _tournamentService.RemoveFromQueue(id);
            return Ok();
        }
    }
}