using System;
using System.Collections.Generic;

namespace Ullechamp_Api.RestApi.Dtos
{
    public class PendingTournamentDTO
    {
        public int TournamentId { get; set; }
        public List<TournamentDTO> Users { get; set; }
        public DateTime Date { get; set; }
    }
}