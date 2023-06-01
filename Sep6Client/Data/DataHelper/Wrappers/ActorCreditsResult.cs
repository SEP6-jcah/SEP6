using System.Text.Json.Serialization;

namespace Sep6Client.Data.DataHelper.Wrappers
{
    public class ActorCreditsResult
    {
        [JsonPropertyName("id")]
        public int MovieId { get; set; }
        
        [JsonPropertyName("title")]
        public string MovieTitle { get; set; }
        
        [JsonPropertyName("release_date")]
        public string? MovieReleaseDate { get; set; }
        
        [JsonPropertyName("vote_average")]
        public double MovieRating { get; set; }
        
        [JsonPropertyName("vote_count")]
        public int Votes { get; set; }
        
        [JsonPropertyName("character")]
        public string? Character { get; set; }
    }
}