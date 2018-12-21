namespace Ullechamp_Api.Core.Entity
{
    public class TournamentUser
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }

        public int Team { get; set; }
    }
}