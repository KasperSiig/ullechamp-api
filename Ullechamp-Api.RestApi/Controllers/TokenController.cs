using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;
using Ullechamp_Api.Infrastructure.Data.Managers;

namespace Ullechamp_Api.RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly TokenManager _tokenManager;

        /// <summary>
        /// Instantiates TokenController
        /// </summary>
        /// <param name="configuration">Contains information about the API configuration</param>
        /// <param name="tokenService">Contains business logic for token</param>
        /// <param name="userService">Contains business logic for user</param>
        public TokenController(IConfiguration configuration,
            ITokenService tokenService,
            IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _tokenManager = new TokenManager(
                configuration["JwtKey"],
                double.Parse(configuration["JwtExpireDays"]),
                configuration["JwtIssuer"]);
        }

        /// <summary>
        /// Fetches tokens from Twitch
        /// </summary>
        /// <param name="code">Code retrieved when logged in with twitch</param>
        /// <returns>A 301 Redirect to the frontend</returns>
        [HttpGet]
        public async Task<ActionResult<string>> Get(string code)
        {
            
            var tokens = await _tokenService.GetTokens(code);
            var accessToken = tokens.GetValue("access_token").ToString();
            var userId = await _tokenService.GetUserId(accessToken);
            
            int id;
            Int32.TryParse(userId, out id);
            
            var userExists =
                _userService.GetUserByTwitchId(id) != null;

            var user = await _tokenService.GetUser(userId, accessToken);
            
            User userCreated = null;
            if (!userExists)
            {
                var data = user.GetValue("data").First;
                var twitchName = data.Value<string>("display_name");
                var imageUrl = data.Value<string>("profile_image_url");
                
                var newUser = new User
                {
                    Twitchname = twitchName,
                    Lolname = "",
                    Role = "Standard",
                    TwitchId = int.Parse(userId),
                    ImageUrl = imageUrl
                };
                
                userCreated = _userService.CreateUser(newUser);
            }

            if (userCreated == null)
                userCreated = _userService.GetUserByTwitchId(int.Parse(userId));

            var jwt = _tokenManager.GenerateJwtToken(userCreated, accessToken);
            
            return Redirect("http://localhost:4200/?token=" + jwt);
        }
    }
}