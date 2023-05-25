using System.Text.Json.Serialization;

namespace Sep6Client.Data.DataHelper.Wrappers
{
    public class ActorResult
    {
        [JsonPropertyName("id")]
        public int PersonId { get; set; }
        
        [JsonPropertyName("cast_id")]
        public int ActorId { get; set; }
        
        [JsonPropertyName("credit_id")]
        public string CreditId { get; set; }
        
        [JsonPropertyName("character")]
        public string Character { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("original_name")]
        public string OriginalName { get; set; }
        
        [JsonPropertyName("popularity")]
        public double PopularityRating { get; set; }
        
        [JsonPropertyName("profile_path")]
        public string ProfileLink { get; set; }
        
        [JsonPropertyName("known_for_department")]
        public string Department { get; set; }
    }
}