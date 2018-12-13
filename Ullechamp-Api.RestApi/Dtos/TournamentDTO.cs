using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.RestApi.Dtos
{
    public class TournamentDTO
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public User User { get; set; }
        public int Team { get; set; }
    }
}