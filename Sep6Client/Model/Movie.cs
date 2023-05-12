using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sep6Client.Model
{
    public class Movie
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [JsonPropertyName("id"), Key]
        public int Id { get; set; }

        [Required, MaxLength(128)]
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [JsonPropertyName("year")]
        public int Year { get; set; }
        
        [JsonConstructor]
        public Movie(int id, string title, int year)
        {
            Id = id;
            Title = title;
            Year = year;
        }
    }
}
