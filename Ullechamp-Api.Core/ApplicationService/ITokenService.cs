using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Ullechamp_Api.Core.ApplicationService
{
    public interface ITokenService
    {
        /// <summary>
        /// Get tokens from twitch api
        /// </summary>
        /// <param name="code">Access Token from twitch</param>
        /// <returns>Tokens</returns>
        Task<JObject> GetTokens(string code);
        
        /// <summary>
        /// Gets user id from twitch
        /// </summary>
        /// <param name="accessToken">Access token to send to Twitch</param>
        /// <returns>User Id</returns>
        Task<string> GetUserId(string accessToken);
        
        /// <summary>
        /// Gets user
        /// </summary>
        /// <param name="userId">Userid to get user from</param>
        /// <param name="accessToken">Access token from Twitch</param>
        /// <returns>User</returns>
        Task<JObject> GetUser(string userId, string accessToken);
    }
}