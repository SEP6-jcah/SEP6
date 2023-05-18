using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sep6Client.Data.TMDB.DataHelper
{
    public class TmdbMovieResult
    {
        [Required]
        [JsonPropertyName("id"), Key]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("original_title")]
        public string? Title { get; set; }

        [Required]
        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }
        
        [Required]
        [JsonPropertyName("overview")]
        public string? Description { get; set; }
        
        // [Required]
        [JsonPropertyName("poster_path")]
        public string? Poster { get; set; }
        
        [JsonPropertyName("original_language")]
        public string? Language { get; set; }
        
        // [Required]
        [Range(0, 10, ErrorMessage = "Please enter a value between {0} and {10}")]
        [JsonPropertyName("vote_average")]
        public float Rating { get; set; }
        
        // [Required]
        [JsonPropertyName("vote_count")]
        public int Votes { get; set; }
        
        // [Required]
        [JsonPropertyName("person_results")]
        public IList<string>? Actors { get; set; }
    }
}