using System;

namespace Ullechamp_Api.Core.Entity
{
    public class User
    {
        public int Id { get; set; }
        public int TwitchId { get; set; }
        public String Twitchname { get; set; }
        public String Lolname { get; set; }
        public String Role { get; set; }
        public String ImageUrl { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public double Kda { get; set; }
        public int WinLoss { get; set; }
        public int Point { get; set; }
        public int Rank { get; set; }
    }
}