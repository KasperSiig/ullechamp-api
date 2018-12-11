using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Ullechamp_Api.Core.ApplicationService;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public TokenController(IConfiguration configuration,
            ITokenService tokenService,
            IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get(string code)
        {
            var tokens = await _tokenService.GetTokens(code);
            var accessToken = tokens.GetValue("access_token").ToString();
            var userId = await _tokenService.GetUserId(accessToken);

            var userExists =
                _userService.GetUserByTwitchId(int.Parse(userId)) != null;

            var user = await _tokenService.GetUser(userId, accessToken);
            if (!userExists)
            {
                var twitchName = user.GetValue("data").First.Value<string>("display_name");
                var imageUrl = user.GetValue("data").First.Value<string>("profile_image_url");
                var newUser = new User
                {
                    Twitchname = twitchName,
                    Lolname = "",
                    Role = "Standard",
                    TwitchId = int.Parse(userId),
                    ImageUrl = imageUrl
                };
                _userService.CreateUser(newUser);
            }
            return Redirect("http://localhost:4200/leaderboard");
        }
    }
}