using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

        [Authorize(Roles = "Standard, Admin")]
        [HttpPost("Queue")]
        public ActionResult<JObject> Post([FromBody] JObject jObject)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtDecoded = handler.ReadJwtToken(jObject.GetValue("jwt").ToString());
            if (jwtDecoded != null)
            {
                var id = jwtDecoded.Claims.FirstOrDefault((p => p.Type == "id")).Value;
                _tournamentService.AddToQueue(id);
                return Ok(id);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Current")]
        public ActionResult<Tournament> Post([FromBody] TournamentDTO tournamentDto)
        {
            /*var id = tournamentDto.User.Id;
            var team = tournamentDto.Team;
            var tourList = _tournamentService.GetUsersInCurrent().OrderBy(t => t.TournamentId);
            var tourId = 1;
            tourId++;
                _tournamentService.AddToCurrent(tourId, id, team);
                _tournamentService.RemoveFromQueue(id);*/
            //var tourId = tourList.First().TournamentId;
            var userId = tournamentDto.User.Id;
            var team = tournamentDto.Team;
            
            _tournamentService.AddToCurrent(userId, team);
            _tournamentService.RemoveFromQueue(userId);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("current")]
        public ActionResult PutCurrent([FromBody] List<TournamentDTO> tournamentDtos)
        {
            var current = _tournamentService.GetUsersInCurrent().Where(t => t.State.Equals(-1)).ToList();
            var newList = new List<TournamentDTO>(tournamentDtos);
            current.ForEach(t =>
            {
                newList.ForEach(tn =>
                {
                    if (t.User.Id.Equals(tn.User.Id))
                    {
                        tournamentDtos.Remove(tn);
                        _tournamentService.RemoveFromQueue(tn.User.Id); 
                    }
                });     
            });
            tournamentDtos.ForEach(dto =>
            {
                
                var userId = dto.User.Id;
                var team = dto.Team;
            
                _tournamentService.AddToCurrent(userId, team);
                _tournamentService.RemoveFromQueue(userId);
            });
            return Ok();
        }

        [Authorize(Roles = "Admin")]
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
                var userWins = oldUser.Wins + 1;
                oldUser.Kills = userKills;
                oldUser.Deaths = userDeaths;
                oldUser.Assists = userAssists;
                oldUser.Wins = userWins;
                updatedUser.Add(oldUser);
            }


            return Ok(_tournamentService.UpdateUser(updatedUser));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Losers")]
        public ActionResult<IEnumerable<UserDTO>> PutLosers([FromBody] UserDTO userDto)
        {
            List<User> updatedUsers = new List<User>();

            foreach (var user in userDto.User)
            {
                var id = user.Id;
                var oldUser = _userService.GetById(id);
                var userKills = oldUser.Kills + user.Kills;
                var userDeaths = oldUser.Deaths + user.Deaths;
                var userAssists = oldUser.Assists + user.Assists;
                var userLosses = oldUser.Losses + 1;
                oldUser.Kills = userKills;
                oldUser.Deaths = userDeaths;
                oldUser.Assists = userAssists;
                oldUser.Losses = userLosses;
                updatedUsers.Add(oldUser);
            }


            return Ok(_tournamentService.UpdateUser(updatedUsers));
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("EndGame")]
        public ActionResult EndGamePut()
        {
            _tournamentService.UpdateState();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Queue/{id}")]
        public ActionResult Delete(int id)
        {
            _tournamentService.RemoveFromQueue(id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("pending")]
        public ActionResult<List<PendingTournamentDTO>> GetPending()
        {
            var tournaments = _tournamentService.GetPending();
            List<PendingTournamentDTO> pending = new List<PendingTournamentDTO>();
            tournaments.ForEach(tournament =>
            {
                pending.Add(new PendingTournamentDTO()
                {
                    TournamentId = tournament.TournamentId,
                    Date = tournament.DateTime
                });
            });
            return Ok(pending);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("pending/{id}")]
        public ActionResult<PendingTournamentDTO> GetPendingById(int id)
        {
            var tournament = _tournamentService.GetPendingById(id);
            var users = _tournamentService.GetUsersInPending(tournament.TournamentId);
            var pending = new PendingTournamentDTO()
            {
                TournamentId = tournament.TournamentId,
                Date = tournament.DateTime,
                Users = new List<TournamentDTO>()
            };
            foreach (var user in users)
            {
                pending.Users.Add(new TournamentDTO()
                {
                    Team = user.Team,
                    User = user.User,
                    TournamentId = tournament.TournamentId
                });
            }
            return Ok(pending);
        }
    }
}