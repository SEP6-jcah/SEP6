using System.Collections.Generic;
using System.Text.Json.Serialization;
using Sep6Client.Model;

namespace Sep6Client.Data.DataHelper.Wrappers
{
    public class CreditListResult
    {
        [JsonPropertyName("id")]
        public int PersonId { get; set; }
        
        [JsonPropertyName("cast")]
        public IList<ActorCreditsResult> ActorCredits { get; set; }

        [JsonPropertyName("crew")] 
        public IList<CrewCreditsResult> CrewCredits { get; set; }
    }
}