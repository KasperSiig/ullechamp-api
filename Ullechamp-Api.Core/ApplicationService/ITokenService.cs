using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Ullechamp_Api.Core.ApplicationService
{
    public interface ITokenService
    {
        Task<JObject> GetTokens(string code);
        Task<string> GetUserId(string accessToken);
        Task<JObject> GetUser(string userId, string accessToken);
    }
}