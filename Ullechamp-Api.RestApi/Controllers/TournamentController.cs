using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;

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

        
        [HttpPost("Queue")]
        public ActionResult<JObject> Post([FromBody]JObject jObject)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtDecoded = handler.ReadJwtToken(jObject.GetValue("jwt").ToString());
            //TODO check for null
            var id = jwtDecoded.Claims.FirstOrDefault((p => p.Type == "id")).Value;
            
            return Ok(id);
        }
        
       /* [HttpDelete("{id}")]
        public ActionResult<> Delete()
        {
            return Ok();
        }*/
    }
}