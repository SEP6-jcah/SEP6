using System.Collections.Generic;

namespace Sep6Client.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Poster { get; set; }
        public float Rating { get; set; }
        public int Votes { get; set; }
        public IList<CrewMember>? Directors { get; set; }
        public IList<CrewMember>? Crew { get; set; }
        public IList<Actor>? Actors { get; set; }
        public string? ReleaseDate { get; set; }
        public string? Language { get; set; }
    }
}