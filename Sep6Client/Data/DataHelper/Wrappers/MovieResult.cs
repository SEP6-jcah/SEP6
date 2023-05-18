using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sep6Client.Data.DataHelper.Wrappers
{
    public class MovieResult
    {
        [Required]
        [JsonPropertyName("id"), Key]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [Required]
        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }
        
        [Required]
        [JsonPropertyName("overview")]
        public string? Description { get; set; }
        
        [Required]
        [JsonPropertyName("poster_path")]
        public string Poster { get; set; }
        
        [Required]
        [JsonPropertyName("original_language")]
        public string Language { get; set; }
        
        [Required]
        [JsonPropertyName("vote_average")]
        public float Rating { get; set; }
        
        [JsonPropertyName("vote_count")]
        public int Votes { get; set; }
        
        [JsonPropertyName("person_results")]
        public IList<string>? Actors { get; set; }
    }
}