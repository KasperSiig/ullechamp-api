using System;

namespace Ullechamp_Api.Core.Entity
{
    public class Queue
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime QueueTime { get; set; }
    }
}