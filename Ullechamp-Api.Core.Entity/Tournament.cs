using System;

namespace Ullechamp_Api.Core.Entity
{
    public class Tournament
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public User User { get; set; }
        public int State { get; set; }
        public int Team { get; set; }
        public DateTime DateTime { get; set; }
    }
}