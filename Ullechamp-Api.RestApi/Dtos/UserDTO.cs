using System.Collections;
using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.RestApi.Dtos
{
    public class UserDTO
    {
        public int TournamentId { get; set; }
        public List<User> User { get; set; }
    }
}