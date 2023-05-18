using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sep6Client.Model
{
    public class TmdbMovie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        
        public string? Poster { get; set; }
        public float Rating { get; set; }
        
        public int Votes { get; set; }
        
        public IList<string>? Actors { get; set; }
        
        // public IList<string>? Directors { get; set; }
        
        public string? ReleaseDate { get; set; }
    }
}