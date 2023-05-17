using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieService.Model
{
    public class Rating
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [JsonPropertyName("movieId"), Key]
        public int MovieId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Please enter a value bigger than {1}")]
        [JsonPropertyName("movieRating")]
        public int MovieRating { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [JsonPropertyName("votes")]
        public int Votes { get; set; }
    }
}