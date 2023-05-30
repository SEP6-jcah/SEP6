using System;

namespace Sep6Client.Model{
    public class Comment
    {
        public int MovieId { get; set; }
        public string? Body { get; set; }
        public string? Username { get; set; }
        public DateTime Timestamp { get; set; }
    }
}