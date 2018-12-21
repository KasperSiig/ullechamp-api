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

        /// <summary>
        /// Instantiates TournamentController
        /// </summary>
        /// <param name="tournamentService">Contains business logic for tournaments</param>
        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        #region Create

        /// <summary>
        /// Adds user to queue
        /// </summary>
        /// <param name="jObject">JWT</param>
        /// <returns>Status Code</returns>
        [Authorize]
        [HttpPost("queue")]
        public ActionResult AddToQueue([FromBody] JObject jObject)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtDecoded = handler.ReadJwtToken(jObject.GetValue("jwt").ToString());

            if (jwtDecoded == null) return BadRequest();

            var id = jwtDecoded.Claims.First((p => p.Type == "id")).Value;
            _tournamentService.AddToQueue(id);
            return Ok();
        }

        #endregion

        #region Read

        /// <summary>
        /// Gets Users in queue
        /// </summary>
        /// <returns>List of Users</returns>
        [HttpGet("queue")]
        public ActionResult<List<User>> GetQueue()
        {
            return Ok(_tournamentService.GetUsersInQueue());
        }

        /// <summary>
        /// Get Users in current 
        /// </summary>
        /// <returns>List of TournamentDTOS</returns>
        [HttpGet("current")]
        public ActionResult<IEnumerable<TournamentDTO>> GetCurrent()
        {
            var usersInCurrent = _tournamentService.GetUsersInCurrent();

            return Ok(usersInCurrent.Select(u => new TournamentDTO()
            {
                TournamentId = u.TournamentId,
                User = u.User,
                Team = u.Team
            }));
        }

        /// <summary>
        /// Gets all tournaments, missing information about kda
        /// </summary>
        /// <returns>List of PendingTournamentDTOs</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("pending")]
        public ActionResult<IEnumerable<PendingTournamentDTO>> GetPending()
        {
            var pending = _tournamentService
                .GetPending()
                .Select(t => new PendingTournamentDTO()
                {
                    TournamentId = t.Id,
                    Date = t.DateTime
                });
            return Ok(pending);
        }
        
        /// <summary>
        /// Gets pending tournament by Id
        /// </summary>
        /// <param name="id">Id to get tournament from</param>
        /// <returns>PendingTournamentDTO</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("pending/{id}")]
        public ActionResult<PendingTournamentDTO> GetPendingById(int id)
        {
            var tournament = _tournamentService.GetPendingById(id);
            var tournamentUsers = _tournamentService.GetUsersInPending(id);
            var pending = new PendingTournamentDTO()
            {
                TournamentId = tournament.Id,
                Date = tournament.DateTime,
                Users = tournamentUsers.Select(tu => new TournamentDTO()
                {
                    Team = tu.Team,
                    User = tu.User,
                    TournamentId = tu.TournamentId
                }).ToList()
            };
            return Ok(pending);
        }

        #endregion

        #region Update

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tournamentDtos"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("current")]
        public ActionResult AddToCurrent([FromBody] List<TournamentDTO> tournamentDtos)
        {
            tournamentDtos.ForEach(dto =>
            {
                var userId = dto.User.Id;
                var team = dto.Team;

                _tournamentService.AddToCurrent(userId, team);
                _tournamentService.RemoveFromQueue(userId);
            });
            return Ok();
        }

        /// <summary>
        /// End pending tournament
        /// </summary>
        /// <param name="tournamentDtos">Contain information about tournament</param>
        /// <param name="team">Winning team</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("end")]
        public ActionResult EndTournament([FromBody] List<TournamentDTO> tournamentDtos,
            [FromQuery] int team)
        {
            var updated = tournamentDtos.Select(td =>
            {
                if (td.Team.Equals(team))
                    td.User.Wins++;
                else
                    td.User.Losses++;

                return td.User;
            }).ToList();

            _tournamentService.UpdateUsers(updated);
            return Ok();
        }

        /// <summary>
        /// End current tournament
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("endgame")]
        public ActionResult EndGamePut()
        {
            _tournamentService.UpdateState(-1, 0);
            return Ok();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes user from queue
        /// </summary>
        /// <param name="id">Id of user to be removed</param>
        /// <returns>Status code/returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("queue/{id}")]
        public ActionResult Delete(int id)
        {
            _tournamentService.RemoveFromQueue(id);
            return Ok();
        }

        #endregion
    }
}