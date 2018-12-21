using System;
using System.Collections.Generic;

namespace Ullechamp_Api.Core.Entity
{
    public class Tournament
    {
        public int Id { get; set; }
        public List<TournamentUser> TournamentUsers { get; set; }
        public int State { get; set; }
        public DateTime DateTime { get; set; }
    }
}