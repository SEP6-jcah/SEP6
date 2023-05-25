using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Sep6Client.Model;

namespace Sep6Client.Data.DataHelper.Wrappers
{
    public class PersonResult
    {
        [Required]
        [JsonPropertyName("id"), Key]
        public int Id { get; set; }
        
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [Required]
        [JsonPropertyName("gender")]
        public int Gender { get; set; }
        
        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }
        
        [JsonPropertyName("biography")]
        public string Biography { get; set; }
        
        [JsonPropertyName("known_for_department")]
        public string Department { get; set; }
        
        [JsonPropertyName("known_for")]
        public Movie[] KnownFor { get; set; }
    }
}