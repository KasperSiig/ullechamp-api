using System;

namespace Ullechamp_Api.Core.Entity
{
    public class User
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String Role { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int Assists { get; set; }
        public double KDA { get; set; }
        public int WinLoss { get; set; }
        public int Point { get; set; }
    }
}