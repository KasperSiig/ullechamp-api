using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Ullechamp_Api.Core.ApplicationService.Impl
{
    public class TokenService : ITokenService
    {
        private IHttpClientFactory _clientFactory;
        private IConfiguration _conf;

        public TokenService(IHttpClientFactory clientFactory,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _conf = configuration;
        }

        public async Task<JObject> GetTokens(string code)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, 
                "https://id.twitch.tv/oauth2/token" +
                "?client_id=" + _conf["Client_ID"] +
                "&client_secret=" + _conf["Client_Secret"] +
                "&code=" + code +
                "&grant_type=authorization_code" +
                "&redirect_uri=" + _conf["JwtIssuer"] + "/token");
            
            var client = _clientFactory.CreateClient();
            
            var response = await client.SendAsync(request);

            return JObject.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<string> GetUserId(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                "https://id.twitch.tv/oauth2/validate");
            request.Headers.Add("Authorization", "OAuth " + accessToken);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            var json = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            return json.GetValue("user_id").ToString();
        }

        public async Task<JObject> GetUser(string userId, string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                "https://api.twitch.tv/helix/users?id=" + userId);
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            return JObject.Parse(response.Content.ReadAsStringAsync().Result);
        }
    }
}