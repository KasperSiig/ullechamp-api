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
        private readonly IConfiguration _conf;
        
        public TournamentController(ITournamentService tournamentService, IConfiguration conf)
        {
            _tournamentService = tournamentService;
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
        
        /*[HttpPut("Current")]
        public ActionResult<Tournament> Put([FromBody] TournamentDTO tournamentDto)
        {
            var 
        }*/
        
        [HttpDelete("Queue/{id}")]
        public ActionResult Delete(int id)
        {
            _tournamentService.RemoveFromQueue(id);
            return Ok();
        }
    }
}